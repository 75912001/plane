# encoding=utf8
import os
import sys
import parse_xls
import shutil


XLS_DIR = ""
CSharp_DIR = ""


def init_path():
	global XLS_DIR
	global CSharp_DIR
	XLS_DIR = check_path(sys.argv[1])
	CSharp_DIR = check_path(sys.argv[2])


def check_path(path):
		# windows os
		if os.name == 'nt':
			ok = path.endswith('\\')
			if ~ok:
				return path + '\\'
			return path
		# unix or linux
		ok = path.endswith('/')
		if ~ok:
			return path + '/'
		return path


def refresh_folder(directory):
	if os.path.exists(directory):
		shutil.rmtree(directory)
	os.makedirs(directory)

class GenerateCSharpFromXls:
	def __init__(self):
		pass

	def tackle_each_file(self):
		for filename in os.listdir(XLS_DIR):
			if ((filename[-3:] != 'xls') and (filename[-4:] != 'xlsx')) or (filename[0] == '~'):
				continue
			self.generate(filename)

	def generate(self, xls_file_name):
		print xls_file_name
		xls_file_path_name = XLS_DIR + xls_file_name
		parse_xls_f = parse_xls.ParseXls(xls_file_path_name)
		csharp_file_path_name = CSharp_DIR + parse_xls_f.staitc_file_name + ".cs"

		csharp_file = file(csharp_file_path_name, "wb")
		content_str = '''/*
*
* Copyright 2016
* All rights reserved.
* This file is automatically generated by python script of which 
* the author is kevinmeng.
*
*/
'''
		content_str += '''
namespace '''
		content_str += parse_xls_f.staitc_file_name + "_auto{\n"
		content_str += parse_xls_f.serialize_structs + '\n'

		content_str += '\tclass ' + parse_xls_f.staitc_file_name + '_cfg_t\n\t{\n'

		print content_str
		for i in range(0, parse_xls_f.get_col_size()):
			type_str = parse_xls_f.get_type(i)
			name_str = parse_xls_f.get_name(i)
			desc_str = parse_xls_f.get_desc(i).replace('\n', '')
			print "111111111111111111111111111"
			print type_str
			print name_str
			print desc_str
			
			if type_str.find('array~') == 0:
				continue
			elif type_str.lower() == 'intarray' or type_str.lower() == 'bytearray':
				content_str += '\t\tstd::vector<uint32_t> ' + name_str + ';' + "\t\t//" + desc_str + "	\n"
			elif type_str.lower() == 'intarray2':
				content_str += '\t\tstd::vector<std::vector<uint32_t>> ' + name_str + ';' + "\t\t//" + desc_str + "	\n"
			else:
				type_str = self.get_repaired_type(type_str.lower())
				content_str += "\t\t" + type_str + " " + name_str + ";" + "\t\t//" + desc_str + "	\n"

		print "2222222222222222222222"
		print content_str
		for relative_variable_name, relative_struct_name in parse_xls_f.map_struct.iteritems():  #for python 2.x
			struct_info = self.get_specific_struct_info(parse_xls_f, relative_struct_name)
			content_str += "\t\tstd::vector<" + relative_struct_name + "_t> " + relative_variable_name + ";" + "\t\t//" + struct_info.desc + "	\n"

		content_str += '\n'
		content_str += '\t\tvoid load(el::lib_file_tab_txt_t& file_tab_txt, std::vector<std::string>& ref){\n'
		content_str += '\t\t\tstd::string __str_def__;\n\n'

		print "3333333333333333333333333"
		print content_str
		for i in range(0, parse_xls_f.get_col_size()):
			type_str = parse_xls_f.get_type(i)
			name_str = parse_xls_f.get_name(i)
			# desc_str = parseXls.get_desc(i)

			if type_str.lower().find('array~') == 0:
				struct_name = self.extract_struct_name(type_str)
				for relative_variable_name, relative_struct_name in parse_xls_f.map_struct.iteritems():  #for python 2.x
					if name_str == relative_variable_name:
						struct_info = self.get_specific_struct_info(parse_xls_f, relative_struct_name)
						content_str += '\t\t\t{\n'
						content_str += '\t\t\t\t' + 'std::string str_array = file_tab_txt.get_val_def(\"' + relative_variable_name + '\", ref, ' + '__str_def__);\n'
						content_str += '\t\t\t\t' + 'auto para_vec = el::g_cat_string<std::string>(' + 'str_array, \';\', \'_\');\n\n'
						content_str += "\t\t\t\t" + "FOREACH(para_vec, it){\n"
						content_str += '\t\t\t\t\t' + relative_struct_name + '_t ' + relative_struct_name + ';\n'
						content_str += '\t\t\t\t\t' + 'std::vector<std::string>& u_vec = *it;\n'

						for i in range(len(struct_info.variables_list)):
							content_str += "\t\t\t\t\t" + 'el::convert_from_string(' + relative_struct_name + '.' + struct_info.variables_list[i] + ', u_vec[' + str(i) + ']);\n'
						content_str += "\t\t\t\t\t" + 'this->' + relative_variable_name + '.push_back(' + relative_struct_name + ');\n'
						content_str += '\t\t\t\t}\n\t\t\t}\n'
			elif type_str.lower() == 'intarray':
				content_str += '\t\t\t{\n'
				content_str += '\t\t\t\t' + 'std::string array = file_tab_txt.get_val_def(\"' + name_str + '\"' + ', ref, __str_def__);\n'
				content_str += '\t\t\t\t' + 'el::g_cat_string(this->' + name_str + ', array, \'_\');\n'
				content_str += '\t\t\t}\n'
			elif type_str.lower() == 'intarray2':
				content_str += '\t\t\t{\n'
				content_str += '\t\t\t\t' + 'std::string array = file_tab_txt.get_val_def(\"' + name_str + '\"' + ', ref, __str_def__);\n'
				content_str += '\t\t\t\t' + 'this->' + name_str + ' = el::g_cat_string<uint32_t>(array, \';\', \'_\');\n'
				content_str += '\t\t\t}\n'
			elif type_str.lower() == 'string':
				content_str += "\t\t\t" + 'this->' + name_str + '= file_tab_txt.get_val_def(\"' + name_str + '\", ref, __str_def__);\n'
			else:
				content_str += "\t\t\t" + 'this->' + name_str + '= file_tab_txt.get_val_def(\"' + name_str + '\", ref, 0);\n'

		content_str = content_str.strip('\n')
		content_str += "\t\t\n\t\t}\n" + "\t};\n" + "}\n"

		csharp_file.write(content_str)
		csharp_file.close()

	def extract_struct_name(self, raw_string):
		struct_name_positin = raw_string.find('~') + 1
		struct_name = []
		for i in range(struct_name_positin, len(raw_string)):
			if (ord(raw_string[i]) == 95) or ((ord(raw_string[i]) >= 48) and (ord(raw_string[i]) <=57)) or ((ord(raw_string[i]) >= 65) and (ord(raw_string[i]) <=90))  or ((ord(raw_string[i]) >= 97) and (ord(raw_string[i]) <=122)):
				struct_name.append(raw_string[i])
			else:
				break
		lst = [word[0].upper() + word[1:] for word in ''.join(struct_name).split()]
		string_struct_name = " ".join(lst)
		string_struct_name = string_struct_name
		return string_struct_name

	def get_repaired_type(self, raw_type):
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

	def get_specific_struct_info(self, parse_xls, struct_name):
		for struct_info in parse_xls.struct_list:
			if struct_info.struct_name == struct_name:
				return struct_info
		return ''

if __name__ == "__main__":
	print '--------------Convert excel to header file---------------'
	init_path()
	refresh_folder(CSharp_DIR)
	generater = GenerateCSharpFromXls()
	generater.tackle_each_file()
	os.system("pause")