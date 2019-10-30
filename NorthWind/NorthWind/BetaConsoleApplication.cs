using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace HomeWork5
{
    public class BetaConsoleApplication
    {
        private DataTable Orders { get; set; }
        private DataTable Employees { get; set; }
        private DataTable Customers { get; set; }
        private DataTable OrderDetails { get; set; }
        private DataTable Products { get; set; }
        private readonly DbProviderFactory providerFactory;
        private DbConnection connection;

        public BetaConsoleApplication(string connectionString, string providerName)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
        }

        public void Begin()
        {
            var dataSet = new DataSet("ShopAutonomousLevel");

            OrdersCreateTable();
            CustomersCreateTable();
            EmployeesCreateTable();
            OrderDetailsCreateTable();
            ProductsCreateTable();

            dataSet.Tables.AddRange(new DataTable[] { Orders, Customers, Employees, OrderDetails, Products });

            dataSet.Relations.Add("EmployeesOrders", Employees.Columns["Id"], Orders.Columns["EmployeeId"]);
            dataSet.Relations.Add("CutomersOrders", Customers.Columns["Id"], Orders.Columns["CustomerId"]);
            dataSet.Relations.Add("OrdersOrderDetails", Orders.Columns["Id"], OrderDetails.Columns["OrderId"]);
            dataSet.Relations.Add("ProductsOrderDetails", Products.Columns["Id"], OrderDetails.Columns["ProductId"]);
            dataSet.AcceptChanges();
        }

        private void OrdersCreateTable()
        {
            Orders = new DataTable("Orders");
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "Id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                Unique = true,
                DataType = typeof(int),
            });
            Orders.PrimaryKey = new DataColumn[] { Orders.Columns["Id"] };

            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "CustomerId",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(Guid)
            });
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "EmployeeId",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(Guid)
            });
            Orders.Columns.Add(new DataColumn
            {
                ColumnName = "OrderDate",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(DateTime)
            });
        }

        private void EmployeesCreateTable()
        {
            Employees = new DataTable("Employees");
            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "Id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                Unique = true,
                DataType = typeof(Guid),
            });
            Employees.PrimaryKey = new DataColumn[] { Employees.Columns["Id"] };

            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "FirstName",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(string)
            });
            Employees.Columns.Add(new DataColumn
            {
                ColumnName = "LastName",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(string)
            });
        }

        private void CustomersCreateTable()
        {
            Customers = new DataTable("Customers");
            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "Id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                Unique = true,
                DataType = typeof(Guid),
            });
            Customers.PrimaryKey = new DataColumn[] { Customers.Columns["Id"] };

            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "FirstName",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(string)
            });
            Customers.Columns.Add(new DataColumn
            {
                ColumnName = "LastName",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(string)
            });
        }

        private void ProductsCreateTable()
        {
            Products = new DataTable("Products");
            Products.Columns.Add(new DataColumn
            {
                ColumnName = "Id",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                Unique = true,
                DataType = typeof(Guid),
            });
            Products.PrimaryKey = new DataColumn[] { Products.Columns["Id"] };

            Products.Columns.Add(new DataColumn
            {
                ColumnName = "ProductName",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(string)
            });
        }

        private void OrderDetailsCreateTable()
        {
            OrderDetails = new DataTable("OrderDetails");
            OrderDetails.Columns.Add(new DataColumn
            {
                ColumnName = "OrderId",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                Unique = true,
                DataType = typeof(Guid),
            });
            OrderDetails.PrimaryKey = new DataColumn[] { OrderDetails.Columns["OrderId"] };

            OrderDetails.Columns.Add(new DataColumn
            {
                ColumnName = "ProductId",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(Guid)
            });
            OrderDetails.Columns.Add(new DataColumn
            {
                ColumnName = "Price",
                Unique = false,
                AllowDBNull = false,
                DataType = typeof(int)
            });
        }
    }
}
