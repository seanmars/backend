<?php

class PerformanceTester
{
    public static function test($func, $count = 10000)
    {
        $timeStart = microtime(TRUE);
        for ($i = 0; $i < $count; $i++) {
            $func();
        }

        $estimatedTime = number_format(microtime(TRUE) - $timeStart, 5);
        return $estimatedTime;
    }
}
