
CREATE TABLE  Customer(
	custId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	custName varchar(50),
	custAge int,
	custAddress varchar (50),
	custEmail varchar (50),
	custPhoneNumber varchar (12)
	-- there is a many to many from cust to order
)


CREATE TABLE Product(
	prodId int IDENTITY(1,1) PRIMARY KEY NOT NULL,	
    prodName varchar(50),
	prodPrice int,
	prodDesc varchar (50),
	prodAgeRestriction int,
)

CREATE TABLE  StoreFront(
	storeId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	storeName varchar(50),
	storeAddress varchar(50) 
)

CREATE TABLE Inventory(
	inventoryId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	prodId int FOREIGN KEY REFERENCES Product(prodId),
	prodQuantitiy int,
	storeId int FOREIGN KEY REFERENCES StoreFront(storeId)
)



CREATE TABLE  LineItem(
	lineItemId int IDENTITY(1,1) PRIMARY KEY,
	prodId int FOREIGN KEY REFERENCES Product(prodId),
	itemQuantity int
)


CREATE TABLE Orders(
	orderId int IDENTITY(1,1) PRIMARY KEY,
	custId int FOREIGN KEY REFERENCES Customer(custId), 
	storeId int FOREIGN KEY REFERENCES StoreFront(storeId)	
)

CREATE TABLE OrderToLineItem(
	orderId int FOREIGN  KEY REFERENCES Orders(orderId),
	lineItemId int FOREIGN KEY REFERENCES LineItem (lineItemId)
)
--------------------------------------------------------------------------------

insert into Customer
values('John Smith', 33, 'Utopia', 'John.Smith@gmail.com', '123-456-7890'),
	('Joe Doe', 42, 'Nowhere', 'JoeDoe@hotmail.com', '987-654-3210'),
	('Shiro Nai', 11, 'Disboard', '[ ]', '000-000-0000')

insert into Product 
values('Pencil', 1, 'Used to write', 0),
	('Paper', 1, 'Used to be written on', 0),
	('Mouse', 50, 'Computer mouse', 5),
	('Laptop', 800, 'Portable PC', 5)

insert into StoreFront 
values('Sprouts', '0000 Sprouts Lane'),
	('Target', '0001 Target Lane'),
	('Costco', '0002 Costco Lane')

	
insert into Inventory
values (1, 50, 1),
	(2, 50, 1),
	(4, 100, 1),
	(1, 25, 2),
	(2, 25, 2),
	(4, 300, 2)


insert into LineItem
values(1, 5),
	(2, 7),
	(4,2),
	(1, 50),
	(2, 50),
	(4,1)


SELECT * FROM LineItem 
ORDER BY id DESC 
LIMIT 1

insert into Orders 
values (1,1),
	(2,2)

insert into OrderToLineItem 
values 	(1,1),
	(1,2),
	(1,3),
	(2,4),
	(2,5),
	(2,6)

select * from Customer

-----------------------------------------------------------------------------------

select * from Customer

select * from StoreFront sf 

select * from Inventory i 

SELECT o.orderId, p.prodId, p.prodName, p.prodPrice, p.prodDesc, p.prodAgeRestriction,  li.itemQuantity, sf.storeAddress
FROM Customer c  
INNER JOIN Orders o ON c.custId = o.custId 
INNER JOIN OrderToLineItem ol ON o.orderId = ol.orderId 
INNER JOIN LineItem li on ol.lineItemId = li.lineItemId 
INNER JOIN Product p ON p.prodId  = li.prodId  
INNER JOIN StoreFront sf ON o.storeId = sf.storeId
WHERE c.custId = 1;



SELECT * FROM Inventory i 
Inner Join Product p ON i.prodId = p.prodId 


select prodId  from Inventory 
Inner Join Product p ON i.prodId = p.prodId
where i.storeId = 1



select i.prodQuantitiy from Inventory i
WHERE inventory.storeId = 1
AND inventory.prodId = 1;


UPDATE Inventory
SET prodQuantitiy = 50 + 5
WHERE inventory.storeId = 1
AND inventory.prodId = 1;

select * from Inventory 




SELECT i.inventoryId  FROM Inventory i 
ORDER BY i.inventoryId  DESC 
OFFSET 0 ROWS FETCH FIRST 1 ROW ONLY



SELECT * FROM Inventory i  
ORDER BY i.inventoryId DESC 
LIMIT 10
)Var1
ORDER BY id ASC;


select top(5) i.inventoryId from Inventory i 
order by i.inventoryId desc
order by i.inventoryId ASC 

insert into LineItem


SELECT o.orderId, li.lineItemId, p.prodId, p.prodName, p.prodPrice, p.prodDesc, p.prodAgeRestriction,  li.itemQuantity, sf.storeAddress
FROM Customer c     
INNER JOIN Orders o ON c.custId = o.custId 
INNER JOIN OrderToLineItem ol ON o.orderId = ol.orderId 
INNER JOIN LineItem li on ol.lineItemId = li.lineItemId 
INNER JOIN Product p ON p.prodId  = li.prodId  
INNER JOIN StoreFront sf ON o.storeId = sf.storeId
where sf.storeId = 1


-------------------------------------------------------------------------------------

DROP TABLE OrderToLineItem  
Drop table Orders  
Drop TABLE Customer 
Drop TABLE Inventory 
DROP TABLE StoreFront  
Drop TABLE LineItem
Drop table Product 


