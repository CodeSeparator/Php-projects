<?php
	function search($filename,$key){
		$descriptor = fopen($filename, 'r');
		$nstr = 0;
		if ($descriptor) {
			$arr = array(0);
			while (($string = fgets($descriptor)) !== false) {
				$temp = ftell($descriptor);
				array_push($arr, $temp);
				$nstr++;// кол-во строк
			}
			$first = 0;
			$last = $nstr-1;
			$x = 0;// флаг найденного значения
			$y = 0;
			while($first <= $last and $x === 0){
				$q = floor(($first+$last) >> 1);
				fseek($descriptor, $arr[$q], SEEK_SET);
				$temp = fread($descriptor, $arr[$q+1]-$arr[$q]);
				$keyval = strstr($temp,"\t",true);
				$ccc = strcmp($key,$keyval);
				if($ccc === 0)
				{
					$y = $temp;
					$x = 1;
				}
				else if($ccc < 0)
				{
					$last = $q-1;
				}
				else
				{
					$first = $q+1;
				}
			}
			if($y === 0 and $x === 0)
				echo "Undefined";
			else {
				echo strstr($y,"\t");
			}
			fclose($descriptor);
		}
		else {
			echo 'Невозможно открыть указанный файл';
		}
	}
	//Пример использования
	$filename = "P:\\openserver\\OSPanel\\domains\\localhost\\test.txt";
	$key = "ключ15";
	search($filename,$key);