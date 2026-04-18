# 🚗 MyDVLD - Manage Driving Licenses With Ease

[![Download MyDVLD](https://img.shields.io/badge/Download-MyDVLD-4B8BBE?style=for-the-badge&logo=github&logoColor=white)](https://github.com/Trillion-pinkandwhiteeverlasting354/MyDVLD)

## 📥 Download MyDVLD

Visit this page to download:  
https://github.com/Trillion-pinkandwhiteeverlasting354/MyDVLD

## 🖥️ What MyDVLD Does

MyDVLD is a Windows desktop app for driving license work. It helps with common tasks such as:

- Adding people and applicant records
- Managing driving license applications
- Tracking test and renewal steps
- Viewing license details in one place
- Keeping records in SQL Server

It uses C#, Windows Forms, SQL Server, and ADO.NET. The app follows a 3-layer design, which helps keep the data, logic, and screens separate.

## ✅ Who This App Is For

MyDVLD suits users who need a simple way to manage driving license workflows on a Windows PC. It can help at:

- Driving license offices
- Training centers
- Front desk work areas
- Small internal admin teams

You do not need coding skills to use the app after it is set up.

## 💻 System Requirements

Use a Windows PC with:

- Windows 10 or Windows 11
- .NET Desktop Runtime or the version required by the app build
- SQL Server installed and running
- Enough disk space for the app and database files
- A mouse and keyboard for normal use

For best results, use a machine with at least:

- 4 GB RAM
- 2 GB free storage
- A stable local Windows account with install rights

## 🧭 What You Need Before You Start

Before you open the app, make sure you have:

- The MyDVLD project files from the link above
- SQL Server installed on your computer
- SQL Server Management Studio if you want to manage the database by hand
- Access to the database scripts or backup used by the app
- Permission to install and run desktop software on your PC

## 📦 Download and Open the Project

1. Go to the download page:  
   https://github.com/Trillion-pinkandwhiteeverlasting354/MyDVLD

2. Download the project files to your computer.

3. Save the files in a folder you can find again, such as:
   - Downloads
   - Desktop
   - Documents

4. If the files come in a ZIP folder, right-click the ZIP file and choose Extract All.

5. Open the extracted folder and look for the project or app files.

## 🛠️ Install SQL Server

MyDVLD uses SQL Server to store all records. If SQL Server is not on your PC yet, install it first.

1. Install SQL Server on your Windows computer.
2. Start the SQL Server service.
3. Make sure you can connect to the server from your machine.
4. Install SQL Server Management Studio if you want an easy way to view the database.

If your team gave you a database backup or script, restore it or run it before opening the app.

## 🗄️ Set Up the Database

To let MyDVLD work correctly, you need a ready database.

1. Open SQL Server Management Studio.
2. Connect to your local SQL Server instance.
3. Restore the database backup, or run the SQL script provided with the project.
4. Check that the database shows up in the list of databases.
5. Make sure the tables and sample data are present if included.

If the app uses a connection string in a config file, update it so it points to your SQL Server name and database name.

## ▶️ Run the Application

After the files and database are ready, open the app.

1. Open the project in Visual Studio if you are using the source files.
2. Build the solution.
3. Start the application.
4. Log in or open the main window, if the app uses one.
5. Check that the screens load and that the app can read data from SQL Server.

If you received a compiled app, open the `.exe` file from the app folder and run it on Windows.

## 🔧 First-Time Setup Checklist

Use this checklist the first time you open MyDVLD:

- Confirm SQL Server is running
- Confirm the database exists
- Check the connection string
- Make sure the app opens without errors
- Test a sample record, such as a person or application entry
- Confirm saved data appears in SQL Server

## 🧩 Main Features

MyDVLD includes tools for license workflow management such as:

- Person record management
- Application tracking
- License status tracking
- Renewal and test flow handling
- Data storage in SQL Server
- Clean screen layout with Windows Forms
- Separated app layers for better code structure

## 🪟 Using the App on Windows

When the app opens, use it like a normal desktop program:

1. Select the area you want to work in.
2. Enter the needed record details.
3. Save the data.
4. Search for records when needed.
5. Open a record to view or update it.

Use the mouse for buttons and forms. Use the keyboard for fields and search boxes.

## 🧾 Common Tasks

### Add a person
1. Open the person form.
2. Enter the person details.
3. Save the record.

### Create a license application
1. Open the application screen.
2. Choose the person.
3. Fill in the application data.
4. Save it.

### Review a license record
1. Search for the person or license.
2. Open the matching record.
3. Check status, dates, and related details.

### Update a record
1. Open the saved entry.
2. Change the needed field.
3. Save the update.

## 🔍 Troubleshooting

If the app does not open, check these items:

- The app files are not blocked by Windows
- SQL Server is running
- The database exists
- The connection string is correct
- You opened the right project or `.exe` file
- Visual Studio has the needed desktop workload if you are building from source

If records do not save, check the database connection and confirm the table names match the app setup.

## 📁 Project Structure

The app uses a 3-layer layout:

- Presentation layer: the Windows Forms screens
- Business layer: the rules and actions
- Data layer: the SQL Server access code

This setup helps keep the app organized and easier to maintain.

## 🔐 Data Handling

MyDVLD stores records in SQL Server, so your data stays in a local database unless you move it elsewhere. Keep backups of the database if you plan to use the app for real work.

## 🧰 For Users Opening the App From Source

If you downloaded the source files and want to run the app in Visual Studio:

1. Install Visual Studio with Windows Forms support.
2. Open the solution file.
3. Restore any needed NuGet packages.
4. Update the database connection string.
5. Build the solution.
6. Start the app.

## 📌 Helpful Checks Before Daily Use

Before you begin normal work each day:

- Start SQL Server
- Open the app
- Check that the main screen loads
- Try a test search
- Confirm saved records still appear in the database

## 📞 Access

Use the download page here:  
https://github.com/Trillion-pinkandwhiteeverlasting354/MyDVLD