<?php

public static function getFileList($dirPath)
{
  if (!file_exists($dirPath)) {
      return array();
  }
  
  return preg_grep('/^([^.])/', scandir($dirPath));
}
