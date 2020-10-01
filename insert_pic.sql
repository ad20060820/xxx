--INSERT INTO Employees (Id, Name, Photo)
--SELECT 10, 'John',
--BulkColumn from Openrowset( Bulk 'C:\photo.bmp', Single_Blob) as EmployeePicture

use FoodMap
update Food 
set "Picture" = BulkColumn from 
Openrowset( Bulk 'C:\Users\User\Desktop\專題\小專1\0918\FoodMap\images\06黑糖青蛙撞奶.jpg', Single_Blob) as Document
where FoodId = 6