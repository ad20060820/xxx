--INSERT INTO Employees (Id, Name, Photo)
--SELECT 10, 'John',
--BulkColumn from Openrowset( Bulk 'C:\photo.bmp', Single_Blob) as EmployeePicture

use FoodMap
update Food 
set "Picture" = BulkColumn from 
Openrowset( Bulk 'C:\Users\User\Desktop\�M�D\�p�M1\0918\FoodMap\images\06�¿}�C�켲��.jpg', Single_Blob) as Document
where FoodId = 6