# -*- coding: gbk -*-
import csv
import os
import sys
import xlrd
import re
import shutil

XLS_DIR = ""
CSV_DIR = ""

def init_path():
	global XLS_DIR
	global CSV_DIR
	XLS_DIR = sys.argv[1]
	CSV_DIR = sys.argv[2]


def refresh_folder(directory):
	if os.path.exists(directory):
                shutil.rmtree(directory)
	os.makedirs(directory)


class ConvertExcelToCsv:
	def __init__(self):
		pass

	def convert_value(self, lst, n_th_row):
		ret = []
		for v in lst:
			data = v
		
			if isinstance(v, int):
				data = int(v)
			elif isinstance(v, float):
				if v == int(v):
					data = int(v)
				else:
					data = str(v)
			elif isinstance(v, unicode):
				v = v.replace("\r\n", " ")
				v = v.replace("\n", " ")
				v = v.strip()
				data = v.encode("gbk")
			else:
				if v.strip() == "":
					if n_th_row != 0:
						raise Exception("有空数据:F_ID为:%d"%lst[0])
					
			data = str(data)
			ret.append(data)
		return ret

	def exclude_wrong_case(self, raw_row):
		d = dict()
		for i in range(len(raw_row)):
			if raw_row[i] in d:
				print 'repeated variable: ', raw_row[i]
				raise Exception('repeated variable name!')
			else:
				d[raw_row[i]] = 0
		
	def csv_from_excel(self, excel_file, ignore_line, out_path):
		src_file = XLS_DIR + excel_file
		print excel_file
		workbook = xlrd.open_workbook(src_file)
		worksheet = workbook.sheet_by_index(0)
		row1 = self.convert_value(worksheet.row_values(0), 0)
		row_n = self.convert_value(worksheet.row_values(ignore_line+1), 0)
		print worksheet.row_values(0),row1 
		print worksheet.row_values(ignore_line+1),row_n
		self.exclude_wrong_case(row_n)
		
		staitc_file_name = row1[0]
		if staitc_file_name == '':
			print excel_file
			raise Exception('file name is nill!')
		csv_file_name = out_path+''.join([staitc_file_name,'.csv'])
		your_csv_file = open(csv_file_name, 'wb')
		wr = csv.writer(your_csv_file, quoting=csv.QUOTE_NONE)
		
		line = 0
		type_list = []
		for rownum in xrange(worksheet.nrows):
			line += 1
			if line <= ignore_line:
				continue
			ret = self.convert_value(worksheet.row_values(rownum), rownum)
			if line == 4:
				type_list = ret
			elif line == 5:
				target_variable_name = []
				variables_name = ret
				for i, t in enumerate(type_list):
					if self.is_split_case(t):
						tmp_list = self.extract_type_name(variables_name[i])
						for j, varName in enumerate(tmp_list):
							tmp_list[j] = self.beautify_formation(varName)
						target_variable_name.extend(tmp_list)
					else:
						name = variables_name[i]
						target_variable_name.append(self.beautify_formation(name))
				self.write_to_object(your_csv_file, target_variable_name)
			else:
				target_val = []
				val_list = ret
				for i, t in enumerate(type_list):
					if self.is_split_case(t):
						target_val.extend(self.extract_content(t, val_list[i]))
					else:
						target_val.append(val_list[i])
				self.write_to_object(your_csv_file, target_val)

		your_csv_file.close()

	def is_split_case(self, raw_string):
		return (raw_string.find(';') > 0) and (raw_string.find('$') < 0)

	def write_to_object(self, fp, raw_list):
		for val in raw_list:
			fp.write('	' + str(val))
		fp.write('\n')
		
	def beautify_formation(self, name_str):
		tmp_name = []
		for i in range(len(name_str)):
			if 'A' <= name_str[i] <= 'Z':
				tmp_name.append('_')
				tmp_name.append(name_str[i].lower())
			else:
				tmp_name.append(name_str[i])
		return ''.join(tmp_name)
	
	def extract_type_name(self, raw_string):
		type_list = []
		type_name = []
		for i in range(len(raw_string)):
			if (((raw_string[i]) == '_') or (ord(raw_string[i]) >= 48) and (ord(raw_string[i]) <=57)) or ((ord(raw_string[i]) >= 65) and (ord(raw_string[i]) <=90))  or ((ord(raw_string[i]) >= 97) and (ord(raw_string[i]) <=122)):
				type_name.append(raw_string[i])
				if i+1 == len(raw_string):
					type_list.append(''.join(type_name))
			elif raw_string[i] == ';':
				type_list.append(''.join(type_name))
				type_name[:] = []
		return type_list
		
	def extract_content(self, raw_type, raw_data):
		type_list = re.split(r'[;]', raw_type)
		#print "type_list = ", type_list
		contentList = re.split(r'[_]', str(raw_data))
		#print "contentList = ", contentList
		original_content_length = len(contentList)
		short_num = len(type_list) - len(contentList)
		#print "short_num = ", short_num
		if short_num > 0:
			for i in range(short_num):
				index = original_content_length + i
				if type_list[index].lower() == "string":
					contentList.append('')
				else:
					contentList.append(0)
		return contentList

if __name__ == "__main__":
	print '--------------Convert excel to csv---------------'
	IGNORE_LINE = 3  # these lines will be ignored
	init_path()
	refresh_folder(CSV_DIR)
	
	convert = ConvertExcelToCsv()
	for filename in os.listdir(XLS_DIR):
		# csv_from_excel(unicode(filename, 'gbk'), unicode(outputName, 'gbk'), IGNORE_LINE, CSV_DIR)
		if ((filename[-3:] != 'xls') and (filename[-4:] != 'xlsx')) or (filename[0] == '~'):
			continue
		convert.csv_from_excel(filename, IGNORE_LINE, CSV_DIR)
	os.system("pause")
