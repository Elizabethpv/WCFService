using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	SqlConnection con = new SqlConnection(@"server=DESKTOP-IT4CQOE\SQLEXPRESS;database=E-commerceASP.NETWebform;integrated security=true");
	public string checkbalence(string accno,string grand_total, string userid)
	{
		string bal = "";
		string qury = "select acc_balance from EC_Account where acc_no='" + accno + "'";
		SqlCommand cmd = new SqlCommand(qury, con);
		con.Open();
		SqlDataReader dr = cmd.ExecuteReader();
		if (dr.Read())
		{
			bal = dr["acc_balance"].ToString();
		}
		con.Close();
		if (!string.IsNullOrEmpty(bal))
		{
			int accbal = Convert.ToInt32(bal);
			int amount=Convert.ToInt32(grand_total);
			if (accbal >= amount)
			{
				int balance = accbal - amount;
				string upbalence = "update EC_Account set acc_balance=" + balance + " where user_id='" + userid + "' and acc_no='" + accno + "'";
				SqlCommand cmds = new SqlCommand(upbalence, con);
				con.Open();
				cmds.ExecuteNonQuery();
				con.Close();
				return "successful";
			}
			else
			{
				return "Insufficient balance";
			}
		}
		else
		{
			return "Account not found";
		}
		
	}

	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
}
