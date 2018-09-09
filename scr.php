<?php

	function search($mass,$key){
		$first = 0;
		$last = count($mass)-1;
		$x = 0;// флаг найденного значения
		$y = 0;
		while($first <= $last and $x === 0){
			$q = floor(($first+$last) >> 1);
			$keyval = strstr($mass[$q],"\t",true);
			$ccc = strcmp($key,$keyval);
			if($ccc === 0)
			{
				$y = $mass[$q];
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
		else{
			echo strstr($y,"\t");
		}
		
	}
	// Пример использования
	$file = file("P:\\openserver\\OSPanel\\domains\\localhost\\test.txt");
	$k = "ключ14";
	
	search($file,$k);