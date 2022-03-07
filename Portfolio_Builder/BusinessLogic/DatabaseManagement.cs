using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Portfolio_Builder.Models;

namespace Portfolio_Builder.BusinessLogic
{
    public class DatabaseManagement
    {
        private readonly MySqlConnection connection;
        private readonly string server;
        private readonly string database;
        private readonly string uid;
        private readonly string password;


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
            Asset asset = new();
            List<AssetDay> assetDays = CreateAssetDayList(assetTickerSymbol);
            if (OpenConnection())
            {
                string sqlQueryText = $"Select symbol, name from Asset where symbol = '{assetTickerSymbol}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    asset = new Asset(assetTickerSymbol, dataReader["Name"].ToString() ?? "","","","", assetDays);
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return asset;
        }

        private List<AssetDay> CreateAssetDayList(string assetTickerSymbol)
        {
            List<AssetDay> assetDays = new();
            AssetDay assetDay;

            if (OpenConnection())
            {
                string sqlQueryText = $"Select Date, Opening_Price, Closing_Price, Daily_High, Daily_Low, Volume from Asset_Data where symbol = '{assetTickerSymbol}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
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

        public Market CreateMarket(string marketName)
        {
            string marketType = String.Empty;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Type from Market where name = '{marketName}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    marketType = dataReader["Type"].ToString() ?? "";
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return new(marketName, marketType, CreateMarketDayList(marketName), GetAssetScoreModels(marketType,marketName));
        }

        private List<MarketDay> CreateMarketDayList(string marketName)
        {
            List<MarketDay> marketDays = new();
            MarketDay marketDay;

            if (OpenConnection())
            {
                string sqlQueryText = $"Select Date, Value from Market_Score_Details where Name = '{marketName}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    marketDay = new MarketDay(ConvertToDateTime(dataReader["Date"].ToString() ?? ""),
                                              ConvertToDouble(dataReader["Value"].ToString() ?? ""));

                    marketDays.Add(marketDay);
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return marketDays;
        }

        public Asset CreateAssetComparableToMarket(string assetTickerSymbol)
        {
            Asset asset = new();
            List<AssetDay> assetDays = CreateAssetDayListComparableToMarket(assetTickerSymbol);
            if (OpenConnection())
            {
                string sqlQueryText = $"Select symbol, name from Asset where symbol = '{assetTickerSymbol}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    asset = new Asset(assetTickerSymbol, dataReader["Name"].ToString() ?? "", "", "", "", assetDays);
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return asset;
        }

        private List<AssetDay> CreateAssetDayListComparableToMarket(string assetTickerSymbol)
        {
            List<AssetDay> assetDays = new();
            AssetDay assetDay;

            if (OpenConnection())
            {
                string sqlQueryText = $"Select Date, Value from Market_Comparables where symbol = '{assetTickerSymbol}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    assetDay = new AssetDay(ConvertToDateTime(dataReader["Date"].ToString() ?? ""), 0,ConvertToDouble(dataReader["Value"].ToString() ?? ""), 0, 0, 0);
                    assetDays.Add(assetDay);
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return assetDays;
        }

        public double GetSingleClosingPrice(string assetTickerSymbol, DateTime date)
        {
            double result = default;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Closing_Price From Asset_Data where symbol = '{assetTickerSymbol}' and Date <= '{date:yyyy-MM-dd HH:mm:ss}' Order By Date Desc Limit 1";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Closing_Price"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = Double.NaN;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return Math.Round(result,2);
        }

        public double GetCurrentPerformance(string marketName, DateTime date)
        {
            double result = default;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Value From Market_Score_Details where Name = '{marketName}' and Date <= '{date:yyyy-MM-dd HH:mm:ss}' Order By Date Desc Limit 1";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Value"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = Double.NaN;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return Math.Round(result, 2);
        }

        public double FindMaxValue(string assetTickerSymbol, DateTime startingDate)
        {
            double result = default;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Max(Daily_High) From Asset_Data where symbol = '{assetTickerSymbol}' and Date >= '{startingDate:yyyy-MM-dd HH:mm:ss}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if(dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Max(Daily_High)"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = Double.NaN;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
        return result;
        }

        public double FindMaxMarketValue(string name, DateTime startingDate)
        {
            double result = default;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Max(Value) From Market_Score_Details where Name = '{name}' and Date >= '{startingDate:yyyy-MM-dd HH:mm:ss}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Max(Value)"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = Double.NaN;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return result;
        }
        public double FindMinValue(string assetTickerSymbol, DateTime startingDate)
        {
            double result = default;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Min(Daily_Low) From Asset_Data where symbol = '{assetTickerSymbol}' and Date >= '{startingDate:yyyy-MM-dd HH:mm:ss}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Min(Daily_Low)"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = Double.NaN;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return result;
        }

        public double FindMinMarketValue(string name, DateTime startingDate)
        {
            double result = default;
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Min(Value) From Market_Score_Details where Name = '{name}' and Date >= '{startingDate:yyyy-MM-dd HH:mm:ss}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    try
                    {
                        result = ConvertToDouble(dataReader["Min(Value)"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                        result = Double.NaN;
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return result;
        }

        public ObservableCollection<MarketScoreModel> GetMarketScoreModels()
        {
            ObservableCollection<MarketScoreModel> marketScores = new();
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Type, Market_Score.Name, Value_Change From Market_Score Join Market On Market_Score.Name = Market.Name Order By Type";
                //string sqlQueryText = $"Select Name, Type From Market Order By Type";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    try
                    {
                        marketScores.Add(new(dataReader["Name"].ToString() ?? "", ConvertToDouble(dataReader["Value_Change"].ToString() ?? "0"), dataReader["Type"].ToString() ?? ""));
                        //marketScores.Add(new(dataReader["Name"].ToString() ?? "", 0, dataReader["Type"].ToString() ?? ""));
                    }
                    catch (Exception)
                    {
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return marketScores;
        }

        public ObservableCollection<AssetScoreModel> GetAssetScoreModels(string type, string typeName)
        {
            ObservableCollection<AssetScoreModel> assetScores = new();
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Asset_Symbol, Category, Name From Asset_Score Join Asset On Asset_Score.Asset_Symbol = Asset.Symbol Where {type} = '{typeName}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    try
                    {
                        assetScores.Add(new(dataReader["Asset_Symbol"].ToString() ?? "", dataReader["Category"].ToString() ?? "", dataReader["Name"].ToString() ?? ""));
                    }
                    catch (Exception)
                    {
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return assetScores;
        }

        public List<string> GetItemsOnWatchlist(string type, string watchlist)
        {
            List<string> assetsOnWatchlist = new();
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Asset_Name From Watchlist Where Type = '{type}' and Watchlist_Name = '{watchlist}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    try
                    {
                        assetsOnWatchlist.Add(dataReader["Asset_Name"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return assetsOnWatchlist;
        }
        public void AddItemToWatchlist(string name,string type, string watchlist)
        {
            if (OpenConnection())
            {
                string sqlQueryText = $"Insert Into Watchlist (Watchlist_Name, Asset_Name, Type) Values ('{watchlist}', '{name}', '{type}')";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch
                { 
                }
                this.CloseConnection();
            }
        }
        public void DeleteItemFromWatchlist(string name, string type, string watchlist)
        {
            if (OpenConnection())
            {
                string sqlQueryText = $"Delete From Watchlist Where Asset_Name = '{name}' and Type = '{type}' and Watchlist_Name = '{watchlist}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                sqlCommand.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public ObservableCollection<string> GetAvalableWatchlists()
        {
            ObservableCollection<string> watchlists = new();
            if (OpenConnection())
            {
                string sqlQueryText = $"Select Watchlist_Name From Watchlist Group By Watchlist_Name";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    try
                    {
                        watchlists.Add(dataReader["Watchlist_Name"].ToString() ?? "");
                    }
                    catch (Exception)
                    {
                    }
                }
                dataReader.Close();

                this.CloseConnection();
            }
            return watchlists;
        }

        public void DeleteWatchlist(string watchlist)
        {
            if (OpenConnection())
            {
                string sqlQueryText = $"Delete From Watchlist Where Watchlist_Name = '{watchlist}'";
                MySqlCommand sqlCommand = new(sqlQueryText, connection);
                sqlCommand.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public static double ConvertToDouble(string value)
        {
            return Math.Round(Convert.ToDouble(value), 2);
        }

        public static DateTime ConvertToDateTime(string value)
        {
            return Convert.ToDateTime(value);
        }
    }
}
