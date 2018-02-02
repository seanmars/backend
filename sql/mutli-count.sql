SELECT
DATE(`birthday`),
SUM(CASE WHEN `sex`=0 THEN 1 ELSE 0 END) AS boy,
SUM(CASE WHEN `sex`=1 THEN 1 ELSE 0 END) AS girl,
FROM `user` GROUP BY DATE(`birthday`);
