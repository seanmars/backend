<?php
// https://stackoverflow.com/questions/42981409/php7-1-json-encode-float-issue/43056278
// https://bugs.php.net/bug.php?id=72567
if (version_compare(phpversion(), '7.1', '>=')) {
    ini_set( 'precision', 17 );
    ini_set( 'serialize_precision', -1 );
}
