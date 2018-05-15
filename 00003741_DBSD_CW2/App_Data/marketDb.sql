IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'3741_DBSD_CW2')
BEGIN
  ALTER DATABASE [3741_DBSD_CW2] SET OFFLINE WITH ROLLBACK IMMEDIATE;
  ALTER DATABASE [3741_DBSD_CW2] SET ONLINE;
  DROP DATABASE [32741_DBSD_CW2];
END

GO

CREATE DATABASE [3741_DBSD_CW2];
GO

USE [3741_DBSD_CW2];
GO

CREATE TABLE Customers (
							customer_id int identity(1,1) NOT NULL,
							title varchar(10) NOT NULL,
							first_name varchar(255) NOT NULL, 
							last_name varchar(255) NOT NULL,
							email varchar(255) NOT NULL,
							pass varchar(255) NOT NULL,
							dob date not null,					 
	constraint staff_primary_key primary key (customer_id),
	--make sure staff cannot be registered unless they are 21 years old
	);

	create table Products(
							product_id int identity(1,1) not null,
							product_name varchar(255) not null,
							product_description varchar(5000),
							price int not null,
							amount_in_stock int not null,	
	constraint product_primary_key primary key (product_id),											
	check (price > 0.1));

	create table Orders(
						order_id int identity(1,1) not null,
						customer_id int not null,
						product_id int,
						order_amount int,
						order_date date,
	constraint order_primary_key primary key (order_id),
	constraint oreder_staff_foreign_key foreign key (customer_id) references Customers(customer_id),
	constraint order_product_foreign_key foreign key (product_id) references Products(product_id),
	check (order_amount > 0));

		
--drop table Orders
--drop table Customers
--drop table Products 

--Electronics (Products_id, product_name, product_description, price, amount_in_stock, warranty_in_months, supplier_id)
insert into Products values('Brentwood','Juicer',45,0);
insert into Products values('Oster','Blender',19,650);
insert into Products values('RCA','Microvawe',64,440);
insert into Products values('Samsung','TV',140,810);
insert into Products values('Samsung','Refrigerator',200,620);
insert into Products values('LG','Gas',200,520);
insert into Products values('LG','Coffee Machine',200,920);
insert into Products values('TEFAL','Kettle',200,720);
insert into Products values('TEFAL','Gas',200,820);
insert into Products values('Keurig','Mixer',200,520);

--DailyProducts (dailyp_id, product_name, product_description, price, amount_in_stock, manufactured_date, expiration_date, supplier_id)
insert into Products values('Angels food','Baton',1,650);
insert into Products values('Multifood','Baton',1.20,590);
insert into Products values('Vivo','Yoghurt 1L',1.90,720);
insert into Products values('Beef','Meat 1kg',12.50,860);
insert into Products values('Chicken','Meat 1kg',9.90,820);
insert into Products values('Mistral','Sugar 2kg',4.90,900);
insert into Products values('Dunyo','Salt 500gr',2.20,1040);
insert into Products values('Alakazay','Black Tea 100p',2.50,515);
insert into Products values('Nesquick','Chocolate milk',1,660);
insert into Products values('Nescafe Classic','Instant coffe 250 gr',9.30,720);

--OtherProducts (otherp_id, product_name, product_description, price, amount_in_stock, supplier_id)
--PK:productid
insert into Products values('BABY NOVA','Baby feeding bottle',7.90,400);
insert into Products values('Selen','Tissue',2.50,1000);
insert into Products values('Zewa','Toilet paper 6p',1.90,1850);
insert into Products values('SG','Dust rag ',1.90,1200);
insert into Products values('Gilette','Razor',1.25,2100);
insert into Products values('Colgate','Toothbrush',1.20,1030);
insert into Products values('Mainstrays','Shower curtain',11.90,2100);
insert into Products values('Dixie','Coffe cup',1.85,1160);
insert into Products values('Rio','Drinkwere 6 set',13.90,2140);
insert into Products values('Corelle','Plate',1.45,2100);

--Customers (Customers_id, first_name, last_name, dob, phone, city, street, house_no, zip, position, salary, branch_id)
--PK: customer_id
insert into Customers values ('Mrs','Qunduz', 'Akramova','qunduz_akramova_80@wqf.cm','123456', '1-2-1980');
insert into Customers values ('Mr','Vohid', 'Rustamov', 'ex@mail.com','123456', '1-2-1970');
insert into Customers values ('Dr','Zokir', 'Nizomiddinov','a@email.com','123456',  '1-2-1980');
insert into Customers values ('Ms','Valeriya', 'Abdullayeva', 'b@email.com','123456', '1-2-1980');
insert into Customers values ('Mrs','Tanya', 'Botirova','c@email.com','123456','1-2-1980');
insert into Customers values ('Ms','Risolat', 'Oripova','d@email.com','123456',  '1-2-1980');
insert into Customers values ('Ms','Saodat', 'Mahsudova','e@email.com','123456',  '1-2-1980');
insert into Customers values ('Dr','Aziza', 'Zumarova','f@email.com','123456',  '1-2-1980');
insert into Customers values ('Mrs','Bahor', 'Qurbonova','g@email.com','123456',  '1-2-1980');
insert into Customers values ('Mr','Donyor', 'Rustamov', 'h@email.com','123456', '1-2-1970');
insert into Customers values ('Mr','Zafar', 'Nizomiddinov','j@email.com','123456',  '1-2-1980');
insert into Customers values ('Ms','Iroda', 'Abdullayeva','k@email.com','123456',  '1-2-1980');
insert into Customers values ('Ms','Anora', 'Botirova','l@email.com','123456',  '1-2-1980');
insert into Customers values ('Dr','Oisha', 'Oripova','m@email.com','123456',  '1-2-1980');
insert into Customers values ('Mrs','Honzoda', 'Mahsudova','n@email.com','123456',  '1-2-1980');
insert into Customers values ('Mr','Madina', 'Zumarova','o@email.com','123456',  '1-2-1980');

update Customers set first_name = 'NoOne' where customer_id = 1;

insert into Orders values (14,2,2,'1-2-2017');
insert into Orders values (3,3,3,'1-2-2017');
insert into Orders values (4,4,4,'1-2-2017');
insert into Orders values (5,5,5,'1-2-2017');
insert into Orders values (6,6,6,'1-2-2017');
insert into Orders values (7,7,7,'1-2-2017');

insert into Orders values (9,1,9,'1-3-2017');
insert into Orders values (8,2,8,'1-4-2017');
insert into Orders values (7,3,7,'1-5-2017');
insert into Orders values (6,4,6,'1-6-2017');
insert into Orders values (5,5,5,'1-7-2017');
insert into Orders values (4,6,4,'1-8-2017');
insert into Orders values (3,7,3,'1-9-2017');

insert into Orders values (14,19,1,'1-1-2017');
insert into Orders values (2,1,3,'1-9-2017');
insert into Orders values (3,1,4,'1-8-2017');
insert into Orders values (4,1,2,'1-10-2017');
insert into Orders values (5,1,6,'1-11-2017');
insert into Orders values (6,1,3,'1-2-2017');
insert into Orders values (7,1,7,'1-6-2017');
insert into Orders (customer_id,product_id,order_amount,order_date) values (1,1,1,'1-2-2017');
insert into Orders (customer_id,product_id,order_amount,order_date) values (1,2,2,'1-2-2017');
insert into Orders (customer_id,product_id,order_amount,order_date) values (16,30,2,'1-2-2017');

insert into Orders values (      14,	19,	1,          '1-1-2017');
insert into Orders values (      8,	5,	4,           '1-1-2017');
insert into Orders values (      15,	6,	1,            '1-1-2017');
insert into Orders values (    7,	21,	5,           '1-1-2017');
insert into Orders values (     7,	19,	1,         '1-1-2017');
insert into Orders values (    1,	11,	3,          '1-1-2017');
insert into Orders values (    8,	21,	5,         '1-1-2017');
insert into Orders values (    2,	30,	2,            '1-1-2017');
insert into Orders values (   9,	6,	5,         '1-1-2017');
insert into Orders values (    5,	26,	5,            '1-1-2017');
insert into Orders values (    6,	3,	5,            '1-1-2017');
insert into Orders values (  5,	1,	2,          '1-1-2017');
insert into Orders values (   1,	1,	3,          '1-1-2017');
insert into Orders values (    2,	1,	1,           '1-1-2017');
insert into Orders values (  13,	27,	3,           '1-1-2017');
insert into Orders values (     14,	25,	2,         '1-1-2017');
insert into Orders values (   16,	20,	5,            '1-1-2017');
insert into Orders values (     12,	19,	1,            '1-1-2017');
insert into Orders values (9,	4,	3,            '1-1-2017');
insert into Orders values (      11,	10,	5,           '1-1-2017');
insert into Orders values (     8,	9,	3,         '1-1-2017');
insert into Orders values (   1,	21,	4,           '1-1-2017');
insert into Orders values (     10,	17,	2,             '1-1-2017');
insert into Orders values (   14,	17,	5,           '1-1-2017');
insert into Orders values (   3,	1,	3,          '1-1-2017');
insert into Orders values (   4,	19,	4,           '1-1-2017');
insert into Orders values (    2,	21,	1,        '1-1-2017');
insert into Orders values (    13,	29,	3,           '1-1-2017');
insert into Orders values (  11,	6,	1,         '1-1-2017');
insert into Orders values (  12,	26,	1,           '1-1-2017');
insert into Orders values (    7,	18,	1,           '1-1-2017');
insert into Orders values (  9,	12,	5,           '1-1-2017');
insert into Orders values (   10,	17,	5,             '1-1-2017');
insert into Orders values (   8,	18,	5,           '1-1-2017');
insert into Orders values (    8,	15,	5,            '1-1-2017');
insert into Orders values (  9,	9,	1,            '1-1-2017');
insert into Orders values (  2,	26,	1,           '1-1-2017');
insert into Orders values (8,	26,	1,           '1-1-2017');
insert into Orders values (    15,	30,	3,          '1-1-2017');
insert into Orders values (    11,	10,	2,          '1-1-2017');
insert into Orders values (15,	20,	3,          '1-1-2017');
insert into Orders values (  11,	22,	1,            '1-1-2017');
insert into Orders values (  8,	15,	4,          '1-1-2017');
insert into Orders values (15,	24,	3,         '1-1-2017');
insert into Orders values (    12,	30,	2,           '1-1-2017');
insert into Orders values (      13,	3,	1,            '1-1-2017');
insert into Orders values (     6,	5,	2,         '1-1-2017');
insert into Orders values (13,	12,	5,            '1-1-2017');
insert into Orders values (    13,	12,	5,           '1-1-2017');
insert into Orders values (    13,	12,	5,            '1-1-2017');
insert into Orders values (       14,19,1,                '1-1-2017');









select * from Products
go

--get all neccessary information for registration and 
--check if email is already exist in database return 1 
--else register the user with provided information
CREATE PROCEDURE register(@title nvarchar(255), @firstname nvarchar(255), @lastname nvarchar(255), @email nvarchar(255), @pass nvarchar(255), @dob date)
as 
begin	
	declare @msg int
	declare @cemail nvarchar(255) 
	declare @cpass nvarchar(255)
	declare crs cursor for select email, pass from Customers
	
	begin 
		open crs	
		fetch next from crs into @cemail, @cpass
		begin
			while @@FETCH_STATUS = 0
				fetch next from crs into @cemail, @cpass						
				if @cemail = @email		
					begin	
						close crs
						deallocate crs	
						set @msg = 1
						return 	@msg
					end
		end										 							 			
			INSERT INTO Customers values (@title, @firstname, @lastname, @email, @pass, @dob) 
			close crs
			deallocate crs
			set @msg = 0
			return @msg	
	end
end

--drop procedure register

go

--get email and password and check with the database if email doesnot exist return -1 if passwords is incorrect return - 2 
--if email and password match return user id
create PROCEDURE signin(@email nvarchar(255), @pass nvarchar(255))
as 
begin	
	declare @msg int
	declare @customer_id int
	declare @cemail nvarchar(255) 
	declare @cpass nvarchar(255)
	declare crs cursor for select email, pass,customer_id from Customers
	
	begin 
		open crs	
		fetch next from crs into @cemail, @cpass,@customer_id 
		begin
			while @@FETCH_STATUS = 0
			begin
				fetch next from crs into @cemail, @cpass,@customer_id
				if @cemail = @email	
					begin	
						if @pass = @cpass
							begin
								close crs
								deallocate crs	
								set @msg = @customer_id								
								return 	@msg
							end
						else
							begin
								close crs
								deallocate crs	
								set @msg = -2
								return 	@msg
							end						
					end	
			end
		end										 							 			
			close crs
			deallocate crs
			set @msg = -1
			return @msg	
	end
end

go

-- update user with a given id number
create PROCEDURE update_customer(@id int , @title nvarchar(255), @firstname nvarchar(255), @lastname nvarchar(255), @email nvarchar(255), @pass nvarchar(255), @dob date)
as 
begin 
	UPDATE Customers 
	SET title = @title, first_name = @firstname, last_name = @lastname, email = @email, pass = @pass, dob = @dob 
	where customer_id = @id
end

--drop procedure update_customer

--execute update_customer 17, 'DR','ss','ss','e@mail.com','12345', '2000-12-12'

--select * from Customers


go
--report
--show suggested items for a customer based on what they ordered
create function suggestion(@customerid int)
returns table 
as 
return
	select Products.product_id, Products.product_name, Products.product_description, Products.price,Products.amount_in_stock, count(Orders.order_id) as 'Number of customers bought'
	from Products 
	full outer join Orders on Products.product_id = Orders.product_id
	where Products.product_id in (select product_id from Products 
	where product_id in
	(select product_id from Orders
	where customer_id in 
						(select customer_id from Orders where product_id in 
								(select product_id from Orders where customer_id = @customerid) ) 
							and product_id not in (select product_id from Orders where customer_id = @customerid)))
	group by Products.product_id, Products.product_name,Products.product_description, Products.price,Products.amount_in_stock


	--select * from suggestion(3)	
	--drop function suggestion

go

-- make order based on supplied info
-- check if the product exist if not return -1
-- check if the amount if order amount is bigger than in stock amount return -2
-- if all condition are met return 0 
create PROCEDURE ordermake (@customer_id int, @product_id int, @order_amount int, @order_date date)
as 
begin
	declare @msg int
	declare @prod_id nvarchar(255) 
	declare @product_in_stock nvarchar(255)
	declare order_cursor cursor for select product_id, amount_in_stock from Products
	begin 
		open order_cursor	
		fetch next from order_cursor into @prod_id, @product_in_stock 
		begin
			while @@FETCH_STATUS = 0
			begin
				fetch next from order_cursor into @prod_id, @product_in_stock
				if @prod_id = @product_id	
					begin	
						if @product_in_stock >= @order_amount
							begin
								close order_cursor
								deallocate order_cursor	
								insert into Orders values (@customer_id,@product_id,@order_amount,@order_date)
								update Products set amount_in_stock = (@product_in_stock - @order_amount) where product_id = @product_id
								set @msg = 0								
								return 	@msg
							end
						else
							begin
								close order_cursor
								deallocate order_cursor	
								set @msg = -2
								return 	@msg
							end						
					end	
			end
		end										 							 			
			close order_cursor
			deallocate order_cursor
			set @msg = -1
			return @msg	
	end
end
go 

create table Logs(
  [customer_id] int,
  [firstname] nvarchar(255),
  [lastname] nvarchar(255),
  [email] nvarchar(255),
  [operation] nvarchar(70),
  [date] datetime,
  [user] nvarchar(40),
)

GO

create TRIGGER customer_trigger
on [dbo].[Customers]
AFTER INSERT, UPDATE
AS
BEGIN
  DECLARE @insertedRows int
  DECLARE @deletedRows int

  SELECT @insertedRows = count(*) from inserted
  SELECT @deletedRows = count(*) from deleted 

  if @deletedRows = 0 
    begin
      INSERT INTO Logs
      select [customer_id], [first_name], [last_name], [email], 'insert', getdate(), User from INSERTED
    end
  else 
    begin
      insert into Logs
      select [customer_id], [first_name], [last_name], [email], 'udate', getdate(), User from DELETED
    end
end

--declare @v int
--execute @v = ordermake 1,2,1,'2000-01-01'
--print @v

go
create nonclustered index index_order on Orders([customer_id]);

create nonclustered index index_product on Orders([product_id]);