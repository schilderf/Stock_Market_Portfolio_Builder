import mysql.connector
import pandas as pd
from msilib.schema import Error

def main():
    # Command to create the mysql docker container: "docker run --name SampleDB -p3306:3306 -e MYSQL_ROOT_PASSWORD=password -d mysql:latest"
    # If you change the root_password, make sure to change it inside the db_config.csv, too!
    df = pd.read_csv("db_config.csv").iloc[-1]
    
    config = {
    'user': df["user"],
    'password': df["password"],
    'host': df["host"]}
    
    try:
        cnxn = mysql.connector.connect(**config)
    except:
        raise Error('Database connection not possible')
    
    cursor = cnxn.cursor()
    
    create_database(cursor)                 
    create_asset_from_df(cursor,pd.read_csv("asset.csv"))
    print("1 of 8 database tables created and populated!")
    create_asset_score_from_df(cursor,pd.read_csv("asset_score.csv"))
    print("2 of 8 database tables created and populated!")
    create_asset_data_from_df(cursor,pd.read_csv("asset_data.csv"))
    print("3 of 8 database tables created and populated!")
    create_market_from_df(cursor,pd.read_csv("market.csv"))
    print("4 of 8 database tables created and populated!")
    create_market_score_from_df(cursor,pd.read_csv("market_score.csv"))
    print("5 of 8 database tables created and populated!")
    create_market_score_details_from_df(cursor,pd.read_csv("market_score_details.csv"))
    print("6 of 8 database tables created and populated!")
    create_market_comparables_from_df(cursor,pd.read_csv("market_comparables.csv"))
    print("7 of 8 database tables created and populated!")
    create_watchlist_from_df(cursor,pd.read_csv("watchlist.csv"))
    print("8 of 8 database tables created and populated!")
    
    cnxn.commit()
    print("Database initialized!")
       

def create_database(cursor):
    try:
        cursor.execute("Drop Database Financial_Market_Database")
    except:
        pass
    
    try:
        cursor.execute("Create Database Financial_Market_Database")
    except:
        pass
    
    cursor.execute("Use Financial_Market_Database")    
    cursor.execute("Create Table Asset (Symbol varchar(50), Name varchar(2048), Sector varchar(256), Industry varchar(256), Country varchar(256), Market_Cap varchar(256), Primary Key (Symbol))")
    cursor.execute("Create Table Asset_Data (Symbol varchar(50), Date date, Closing_Price double, Primary Key (Symbol, Date))")
    cursor.execute("Create Table Asset_Score (Asset_Symbol varchar(50), Score double, Category varchar(256), Primary Key (Asset_Symbol))")
    cursor.execute("Create Table Market (Name varchar(256), Type varchar(50), Primary Key (Name))")
    cursor.execute("Create Table Market_Score (Name varchar(256), Value_Change double, Primary Key (Name))")
    cursor.execute("Create Table Market_Score_Details (Name varchar(256), Date date, Value double, Primary Key (Name, Date))")
    cursor.execute("Create Table Market_Comparables (Symbol varchar(50), Date date, Value double, Primary Key (Symbol, Date))")
    cursor.execute("Create Table Watchlist (Watchlist_Name varchar(256), Asset_Name varchar(256), Type varchar(50), Primary Key (Watchlist_Name, Asset_Name, Type))")
  
def create_asset_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Asset (Symbol,Name,Sector,Industry,Country, Market_Cap) Values ('{row['Symbol']}','{row['Name']}','{row['Sector']}','{row['Industry']}','{row['Country']}','{row['Market_Cap']}')")

def create_asset_score_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Asset_Score (Asset_Symbol,Score,Category) Values ('{row['Asset_Symbol']}',{row['Score']},'{row['Category']}')")
        
def create_asset_data_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Asset_Data (Symbol,Date,Closing_Price) Values ('{row['Symbol']}','{row['Date']}',{row['Closing_Price']})")
    
def create_market_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Market (Name,Type) Values ('{row['Name']}','{row['Type']}')")
        
def create_market_score_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Market_Score (Name,Value_Change) Values ('{row['Name']}',{row['Value_Change']})")
        
def create_market_score_details_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Market_Score_Details (Name,Date,Value) Values ('{row['Name']}','{row['Date']}',{row['Value']})")
    
def create_market_comparables_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Market_Comparables (Symbol,Date,Value) Values ('{row['Symbol']}','{row['Date']}',{row['Value']})")
    
def create_watchlist_from_df(cursor,df):
    for index, row in df.iterrows():
        cursor.execute(f"Insert Into Watchlist (Watchlist_Name,Asset_Name,Type) Values ('{row['Watchlist_Name']}','{row['Asset_Name']}','{row['Type']}')")
    
if __name__ == "__main__":
  main()