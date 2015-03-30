CREATE trigger trCheckDublicatePK
on Customer
for insert
as 
begin
	declare @telnr varchar(10)
	declare @ssn varchar(10)
	select @telnr = telnr from inserted
	select @ssn = ssn from inserted

	declare @ok as int
	set @ok = 1

	
	select @ok = 0
	where
		exists(	
			select ssn
			from customer
			where telnr = @telnr
			except
			select ssn
			from Customer
			where ssn = @ssn
			)
	if(@ok = 0)
	begin
		raiserror('Varning: Telefonnumret existerar redan i databasen', 12, 1)
	end
end


CREATE procedure spDeleteCustomer
@ssn varchar(20)
as
begin
	begin transaction
		begin try
			delete FROM  dbo.Customer
			where ssn = @ssn
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spDeleteFlower
@flowerNr varchar(20)
as
begin
	begin transaction
		begin try
			delete FROM  dbo.Flower
			where flowerNr = @flowerNr
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spDeleteOrder
@order_nr varchar(20)
as
begin
	begin transaction
		begin try
			delete FROM  dbo.OrderTable
			where orderNr = @order_nr
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end

CREATE procedure spGetAllCustomers
as
begin 
	begin transaction
		begin try
			select *
			from Customer
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spGetAllFlowers 
as
begin 
	begin transaction
		begin try
			select *
			from Flower
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spGetCustomerOrders
@ssn varchar(20)
as
begin 
	begin transaction
		begin try
			select *
			from OrderTable
			where @ssn = ssn
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spGetFlowerPrice
@flower_nr varchar(20)
as
begin 
	begin transaction
		begin try

		select unitPrice 
		from Flower 
		where flowerNr = @flower_nr
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spGetOrderLines
@orderNr varchar(20)
as
begin 
	begin transaction
		begin try
			select *
			from OrderLine
			where @orderNr = orderNr
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spGetOrders
as
begin 
	begin transaction
		begin try
			select ssn
			from OrderTable
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spSetCustomer

@ssn VARCHAR (20),
@first_name VARCHAR(20),
@last_name VARCHAR(20),
@tel_nr VARCHAR(20)
as 
begin
	begin transaction
		begin try
			insert into dbo.Customer
			values (@ssn, @first_name, @last_name, @tel_nr)
			commit transaction
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			raiserror('Fel i stored procedure spSetCustomer', 12, 1)
			rollback transaction
		end catch
end


CREATE procedure spSetFlower

@flower_nr VARCHAR(20),
@unit_price int,
@color VARCHAR(20),
@name VARCHAR(20)
as 
begin
	begin transaction
		begin try
			insert into dbo.Flower
			values (@flower_nr, @unit_price, @color, @name)
			commit transaction
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			raiserror('Fel i stored procedure spSetFlower', 12, 1)
			rollback transaction
		end catch
end

CREATE procedure spSetOrder
@order_nr VARCHAR(20),
@ssn VARCHAR(20),
@delivery_address VARCHAR(20),
@total_price int
as 
begin
	begin transaction
		begin try
			insert into dbo.OrderTable
			values (@order_nr, @ssn, @delivery_address, @total_price)
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end


CREATE procedure spSetOrderLine
@order_nr VARCHAR (20),
@flower_nr VARCHAR(20),
@quantity int,
@order_line_price int
as 
begin
	begin transaction
		begin try
			insert into dbo.OrderLine
			values ( @order_nr, @flower_nr, @quantity, @order_line_price)
		end try
		begin catch
			print 'Transaction rollback'
			print ERROR_MESSAGE()
			rollback transaction
		end catch
	commit transaction
end
