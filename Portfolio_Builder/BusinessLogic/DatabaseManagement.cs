using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Portfolio_Builder.BusinessLogic
{
    public class DatabaseManagement
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;


        public DatabaseManagement()
        {
            server = "localhost";
            database = "Market_Data";
            uid = "root";
            password = "password";
            string connectionString = $"Server={server}; Database={database};Uid={uid};Password={password}";

            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Asset CreateAsset(string assetTickerSymbol)
        {
            Asset asset = new Asset();
            List<AssetDay> assetDays = CreateAssetDayList(assetTickerSymbol);
            if (OpenConnection())
            {
                string sqlQueryText = $"Select symbol, name from Asset where symbol = '{assetTickerSymbol}'";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    asset = new Asset(assetTickerSymbol, dataReader["Name"].ToString() ?? "", assetDays);
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return asset;
        }

        private List<AssetDay> CreateAssetDayList(string assetTickerSymbol)
        {
            List<AssetDay> assetDays = new List<AssetDay>();
            AssetDay assetDay = new AssetDay();

            if (OpenConnection())
            {
                string sqlQueryText = $"Select Date, Opening_Price, Closing_Price, Daily_High, Daily_Low, Volume from Asset_Data where symbol = '{assetTickerSymbol}'";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    assetDay = new AssetDay(ConvertToDateTime(dataReader["Date"].ToString() ?? ""),
                                                     ConvertToDouble(dataReader["Opening_Price"].ToString() ?? ""),
                                                     ConvertToDouble(dataReader["Closing_Price"].ToString() ?? ""),
                                                     ConvertToDouble(dataReader["Daily_High"].ToString() ?? ""),
                                                     ConvertToDouble(dataReader["Daily_Low"].ToString() ?? ""),
                                                     ConvertToDouble(dataReader["Volume"].ToString() ?? ""));

                    assetDays.Add(assetDay);
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return assetDays;

        }

        public double FindMaxValue(string assetTickerSymbol, DateTime startingDate)
        {
            double result = 0.0;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Max(Daily_High) From Asset_Data where symbol = '{assetTickerSymbol}' and Date > '{startingDate.ToString("yyyy-MM-dd HH:mm:ss")}'";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if(dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Max(Daily_High)"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = 0.0;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return result;
        }
        public double FindMinValue(string assetTickerSymbol, DateTime startingDate)
        {
            double result = 0.0;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Min(Daily_Low) From Asset_Data where symbol = '{assetTickerSymbol}' and Date > '{startingDate.ToString("yyyy-MM-dd HH:mm:ss")}'";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Min(Daily_Low)"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = 0.0;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return result;
        }
        public double ConvertToDouble(string value)
        {
            return Convert.ToDouble(value);
        }

        public DateTime ConvertToDateTime(string value)
        {
            return Convert.ToDateTime(value);
        }
    }
}
