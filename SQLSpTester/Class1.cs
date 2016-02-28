using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace SQLSpTester
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestSaleCity()
        {

            // Set up a command object
            SqlCommand comm = new SqlCommand();

            // Set up the connection
            comm.Connection = new SqlConnection(
            @"server=.\SQLExpress; trusted_connection=true; database=AppDB");

            // Specify Procedure Name
            comm.CommandText = "dbo.SaleCity";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@City", "Chennai");

            // Create a DataSet for storing the results
            DataSet ds = new DataSet();

            // Define a DataAdapter to fill a DataSet
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comm;
            try
            {
                // Fill the dataset
                adapter.Fill(ds);

            }
            catch
            {
                Assert.Fail("Exception occurred!");
            }

            // Validate Actual Results
            Assert.IsTrue(ds.Tables.Count == 1,
            "Result set count != 1");
            DataTable dt = ds.Tables[0];

            // There must be exactly four columns returned
            Assert.IsTrue(
            dt.Columns.Count == 4,
            "Column count != 4");

            // There must be columns City, SaleValue, SaleDate and Name
            Assert.IsTrue(
            dt.Columns.IndexOf("City") > -1,
            "Column City does not exist");

            Assert.IsTrue(
            dt.Columns.IndexOf("SaleValue") > -1,
            "Column SaleValue does not exist");

            Assert.IsTrue(
            dt.Columns.IndexOf("SaleDate") > -1,
            "Column SaleDate does not exist");

            Assert.IsTrue(
            dt.Columns.IndexOf("Name") > -1,
            "Column Name does not exist");

            // There must be more than one row returned
            Assert.IsTrue(
            dt.Rows.Count >= 1,
            "Result rows returned");
        }

    }
}