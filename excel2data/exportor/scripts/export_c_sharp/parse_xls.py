# encoding=utf8
import re
import sys
import xlrd
import time

reload(sys)  
sys.setdefaultencoding('utf8')

class ParseXls:
	def __init__(self, xls_full_path_name):
		self.xls_full_path_name = xls_full_path_name
		self.staitc_file_name = ''
		self.col_size = 0
		self.row_size = 0
		self.type = []
		self.name = []
		self.desc = []
		self.struct_list = []
		self.map_struct = {}
		self.serialize_structs = ''
		
		workbook = xlrd.open_workbook(self.xls_full_path_name)
		worksheet = workbook.sheet_by_index(0)
		row1 = self.convert_value(worksheet.row_values(0))
		
		row2 = self.convert_value(worksheet.row_values(1))
		row3 = self.convert_value(worksheet.row_values(2))
		row4 = self.convert_value(worksheet.row_values(3))
		row5 = self.convert_value(worksheet.row_values(4))
		print row1
		print row2
		print row3
		print row4
		print row5

		self.exclude_repeated_case(row5)

		if row1[0] == '':
			print '!!!!!!!! ', 'File name is nill!', ' !!!!!!!!!!'
			time.sleep(1000)
		self.staitc_file_name = self.beautify_name(row1[0])
		
		for i in range(len(row2)):
			if row2[i] == "1":
				str_name = row4[i].lower()
				#if str_name == "intarray" or str_name == "intarray2":
				#	str_name = "String"
				self.exclude_blank_data(row4[i], 4)
				self.exclude_blank_data(row5[i], 5)
					
				if self.is_split_case(str_name):
					type_list = str_name.split(';')
					variables_list = row5[i].split(';')
					if len(type_list) != len(variables_list):
						print '!!!!!!!!!!!! ', 'encounter error here. mark 1', '!!!!!!!!!!!!!!!'
						time.sleep(1000)
					for j in range(len(type_list)):
						t = self.trim_end_blank(type_list[j].lower())
						variable = self.trim_end_blank(variables_list[j])
						desc = self.trim_end_blank(unicode(row3[i], 'gbk'))
						
						self.type.append(t)
						
						self.name.append(self.beautify_formation(variable))
						self.desc.append(desc)
				else:
					t = self.trim_end_blank(str_name.lower())
					variable = self.trim_end_blank(row5[i])
					desc = self.trim_end_blank(unicode(row3[i], 'gbk'))
					
					self.type.append(t)
					self.name.append(self.beautify_formation(variable))
					self.desc.append(desc)
		self.col_size = len(self.type)
		print self.col_size 
		self.row_size = worksheet.nrows - 5
		self.extract_struct_info()
		
	
	def repair_type(self, raw_type):
		print raw_type
#		if raw_type.lower() == "uint32"
#			return "uint32"
		if raw_type.lower() == "byte":
			return "uint8_t"
		elif raw_type.lower() == "int":
			return "uint32_t"
		elif raw_type.lower() == "string":
			return "std::string"
		elif raw_type.lower() == "short":
			return "uint16_t"
		elif raw_type.lower() == "float":
			return "double"
		elif (raw_type.lower() == "intarray") or (raw_type.lower() == "bytearray"):
			return "Array"
		elif raw_type.lower() == "intarray2":
			return "Array = []"

	def is_split_case(self, raw_string):
		return (raw_string.find(';') > 0) and (raw_string.find('$') < 0)

	def extract_struct_info(self):
		for i in range(len(self.type)):
			if self.type[i].find('array~') >=0:
				struct_name = self.extract_class_name(self.type[i])
				type_list = self.extract_type_name(self.type[i])
				variables_list = self.extract_variables_name(self.type[i])
				
				struct_info = StructInfo()
				struct_info.struct_name = struct_name.lower()
				struct_info.type_list = type_list
				struct_info.variables_list = variables_list
				struct_info.desc = self.desc[i].replace('\n','')
				struct_info.relative_variable_name = self.name[i]
				self.map_struct[self.name[i]] = struct_info.struct_name
				
				whether_continue = False
				for struct_value in self.struct_list:
					if struct_value.struct_name == struct_name.lower():
						whether_continue = True
						break
				if whether_continue:
					continue    #if the class has appeared in self.struct_list, just continue.
				self.struct_list.append(struct_info)   #store new struct_info for later use

				#print "type_list = ", type_list
				#print "variables_list = ", variables_list
				if len(type_list) != len(variables_list):
					print "encounter some error here. mark 1."
					return 
				
				serialize_struct = ''
				serialize_struct += '\tstruct ' + struct_info.struct_name + "_t{\n"
				for i in range(len(struct_info.type_list)):
					serialize_struct += '\t\t' + self.repair_type(struct_info.type_list[i]) + " " + struct_info.variables_list[i] + ";\n"
				serialize_struct += "\t};\n"
				self.serialize_structs += serialize_struct
		
	def extract_class_name(self, raw_string):
		class_name_positin = raw_string.find('~') + 1
		struct_name = []
		for i in range(class_name_positin, len(raw_string)):
			if (ord(raw_string[i]) == 95) or ((ord(raw_string[i]) >= 48) and (ord(raw_string[i]) <=57)) or ((ord(raw_string[i]) >= 65) and (ord(raw_string[i]) <=90))  or ((ord(raw_string[i]) >= 97) and (ord(raw_string[i]) <=122)):
				struct_name.append(raw_string[i])
			else:
				break
		lst = [word[0].upper() + word[1:] for word in ''.join(struct_name).split()]
		string_class_name = " ".join(lst)
		#string_class_name = self.beautify_formation(string_class_name)
		string_class_name = string_class_name
		return string_class_name

	def extract_type_name(self, raw_string):
		tmp_list = []
		type_list = []
		#it seems that the first type name always appears after '$' symble. 
		dollor_positon = raw_string.find('$')
		type_name = []
		for i in range(dollor_positon + 1, len(raw_string)):
			if ((ord(raw_string[i]) >= 48) and (ord(raw_string[i]) <=57)) or ((ord(raw_string[i]) >= 65) and (ord(raw_string[i]) <=90))  or ((ord(raw_string[i]) >= 97) and (ord(raw_string[i]) <=122)):
				type_name.append(raw_string[i])
			else:
				tmp_list.append(''.join(type_name))
				break
		#from the second type name, it seems that the type name always appears after ';' symble.
		#find all the position of semicolon in string name.
		semicolon_positin_list = [m.start() for m in re.finditer(';', raw_string)]
		#print "semicolon_positin_list = ",semicolon_positin_list
		for semicolon_positin in semicolon_positin_list:
			type_name[:] = [] #clear content of type_name
			for j in range(semicolon_positin + 1, len(raw_string)):
				if ((ord(raw_string[j]) >= 48) and (ord(raw_string[j]) <=57)) or ((ord(raw_string[j]) >= 65) and (ord(raw_string[j]) <=90)) or ((ord(raw_string[j]) >= 97) and (ord(raw_string[j]) <=122)):
					type_name.append(raw_string[j])
				else:
					tmp_list.append(''.join(type_name))
					break
					
		for rawType in tmp_list:
			#type_list.append(self.repair_type(rawType.lower()))
			type_list.append(rawType.lower())
		return type_list

	#typeString has the form 'array~channel_type$short~map_id;int~x;int~y'
	def extract_variables_name(self, raw_string):
		variables_list = []
		variable_name = []
		wave_position_list = [m.start() for m in re.finditer('~', raw_string)]
		for i in range(len(wave_position_list)):
			if i == 0:
				continue #the first ~ symble is just a mark for the form 'array~channel_type$short~map_id;int~x;int~y'
			variable_name[:] = []
			for j in range(wave_position_list[i] + 1, len(raw_string)):
				if (ord(raw_string[j]) == 95) or ((ord(raw_string[j]) >= 48) and (ord(raw_string[j]) <=57)) or ((ord(raw_string[j]) >= 65) and (ord(raw_string[j]) <=90)) or ((ord(raw_string[j]) >= 97) and (ord(raw_string[j]) <=122)):
					variable_name.append(raw_string[j])
				else:
					break
			#variables_list.append(self.beautify_formation(''.join(variable_name)))
			variables_list.append(''.join(variable_name))
		return variables_list

	def beautify_formation(self, name_str):
		tmp_name = []
		for i in range(len(name_str)):
			if name_str[i] >= 'A' and name_str[i] <= 'Z':
				if i != 0:
					tmp_name.append('_')
				tmp_name.append(name_str[i].lower())
			else:
				tmp_name.append(name_str[i])
		return ''.join(tmp_name)

	def get_type(self, col_index):
		return self.type[col_index]

	def get_name(self, col_index):
		return self.name[col_index]

	def get_desc(self, col_index):
		return self.desc[col_index]

	def get_col_size(self):
		return self.col_size

	def convert_value(self, lst):
		ret = []
		for v in lst:
			data = v
			if isinstance(v, int):
				data = int(v)
			if isinstance(v, float):
				if v == int(v):
					data = int(v)
				else:
					data = float(v)
			if isinstance(v, unicode):
				data = v.encode("gbk")
			data = str(data)
			ret.append(data)
		return ret

	def exclude_repeated_case(self, raw_row):
		d = dict()
		for i in range(len(raw_row)):
			if raw_row[i] in d:
				print '!!!!!!!!!! repeated variable: ', raw_row[i] , '!!!!!!!!!!!!'
				time.sleep(1000)

			else:
				d[raw_row[i]] = 0

	def exclude_blank_data(self, raw_data, row):
		if raw_data == '':
			errStr = 'There is blank data in row ' + str(row)
			print '!!!!!!!!!!!! ', errStr, ' !!!!!!!!!!!'
			time.sleep(1000)
		return

	def beautify_name(self, raw_string):
		'''convert string the form of 'SkillLeader' to the form of 'skill_leader'.'''
		for i in range(1, len(raw_string)):
			if (ord(raw_string[i]) < 65 and ord(raw_string[i]) > 90):
				return  #all of letters of raw_string are not uppercase, so return outright.
		struct_name = []
		struct_name.append(raw_string[0].lower())
		for i in range(1, len(raw_string)):
			struct_name.append(raw_string[i].lower())
			if i + 1 == len(raw_string):
				return ''.join(struct_name)
			if ord(raw_string[i+1]) >= 65 and ord(raw_string[i+1]) <= 90:
				struct_name.append('_')
				continue

	def trim_end_blank(self, raw_string):
		return raw_string.rstrip()
		

class StructInfo:
	def __init__(self):
		self.struct_name = ''
		self.type_list = []
		self.variables_list = []
		self.desc = []
		self.relative_variable_name = ''
#dat = Dat("/Users/leowu/test/source/StaticErrorCodeInfo.dat")
#parseXls = ParseXls("X:\\clientTools\\tools\\craft_overall.xls")
'''
print "---------------------"
for i in range(len(dat.name)):
	print dat.name[i]
print "---------------------"
for i in range(len(dat.type)):
	print dat.type[i]
print "---------------------"
for i in range(len(dat.desc)):
	print dat.desc[i]
print "---------------------"
print dat.classesSerialize
'''
