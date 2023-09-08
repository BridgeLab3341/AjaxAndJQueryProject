Create Database AjaxAndJQuery

Create Table Employee (Id int Primary key identity(1,1),
Name Varchar(50),
State Varchar(50),
City Varchar(50),
Salary Bigint);

Create or Alter Procedure SpAddEmployeeData(
@Name varchar(50),
@State Varchar(50),
@City Varchar(50),
@Salary Bigint)
As
Begin
Begin Try
Insert into Employee (Name,State,City,Salary) values(@Name,@State,@City,@Salary);
End Try
Begin Catch
Select Error_Message() as ErrorMessage;
End catch;
End;

Create or Alter Procedure SpGetAll
As
Begin
Begin Try 
Select * from Employee;
End Try
Begin Catch
SELECT ERROR_MESSAGE() AS ErrorMessage;
End Catch;
End;

Create or Alter Procedure SpUpdateEmployee(
@Name varchar(50),
@State Varchar(50),
@City Varchar(50),
@Salary Bigint)
As
Begin
Begin Try
Update Employee Set Name=@Name,State=@State,City=@City,Salary=@Salary;
End Try
Begin Catch
Select ERROR_MESSAGE() as ErrorMessage;
End Catch;
End


Create or Alter Procedure SPDeleteEmployee(
@Id Int)
As
Begin
Begin Try
Delete from Employee where Id=@Id;
End Try
Begin Catch
Select ERROR_MESSAGE() as ErrorMessage;
End Catch;
End