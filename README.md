# Dashboard Application

This project is a dashboard application developed using C# with Material Skin Framework, Entity Framework, and LiveCharts. It allows users to interact with the database and visually track data. The dataset used in this project is the [Turkish Market Sales Dataset](https://www.kaggle.com/datasets/omercolakoglu/turkish-market-sales-dataset-with-9000items) from Kaggle.





https://github.com/serkanyasr/CSharp-DashBoard-EntityFrameWork/assets/80819652/3e05c9c6-7737-4670-b026-eaab4af50075







## Getting Started

This guide explains how to set up and run the project on your local machine.

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) (2019 or later)
- .NET Framework (4.7.2 or later)
- SQL Server (LocalDB or full version)

### Installation

1. **Clone the repository:**

    ```sh
    https://github.com/serkanyasr/CSharp-DashBoard-EntityFrameWork.git
    cd CSharp-DashBoard-EntityFrameWork
    ```

2. **Open the project in Visual Studio:**

    - Open the `DASHBOARD.sln` file with Visual Studio.

3. **Install the required NuGet packages:**

    - Right-click on the project and select "Manage NuGet Packages".
    - In the "Browse" tab, search for and install the following packages:
      - MaterialSkin.2
      - EntityFramework
      - LiveCharts.Wpf

4. **Configure the database connection:**

    - Update the connection string in the `App.config` or `Web.config` file with your database details.

    ```xml
    <connectionStrings>
      <add name="DefaultConnection" connectionString="Server=(localdb)\\mssqllocaldb;Database=DashboardDB;Trusted_Connection=True;" providerName="System.Data.SqlClient"/>
    </connectionStrings>
    ```

5. **Apply database migrations:**

    - Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
    - Run the following command to update the database:

    ```sh
    Update-Database
    ```

6. **Load the dataset:**

    - Download the [Turkish Market Sales Dataset](https://www.kaggle.com/datasets/omercolakoglu/turkish-market-sales-dataset-with-9000items) from Kaggle.
    - Import the dataset into your SQL Server database. You can use SQL Server Management Studio (SSMS) or a script to load the data into the corresponding tables.

## Usage

1. **Start the application:**

    - Press F5 or click the "Start" button to run the project.

2. **Use the dashboard screen:**

    - When the application starts, you will be greeted with a user-friendly interface. From here, you can add, update, and delete data in the database.

## Project Structure

- **Entities**: Classes corresponding to database tables.
- **Data**: Entity Framework DbContext class and database operations.
- **UI**: Forms and components related to the user interface. This includes visual elements and charts created using LiveCharts.

## Dataset

The dataset used in this project is the [Turkish Market Sales Dataset](https://www.kaggle.com/datasets/omercolakoglu/turkish-market-sales-dataset-with-9000items). It includes sales data for 9000 items in a Turkish market. The dataset is structured with various attributes that help in analyzing market sales trends.

## Contributing

We welcome contributions! Please open an issue first to discuss what you would like to change before submitting a pull request.

1. **Fork** the repository
2. **Create** your feature branch (`git checkout -b feature-name`)
3. **Commit** your changes (`git commit -m 'Add some feature'`)
4. **Push** to the branch (`git push origin feature-name`)
5. **Open** a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

If you have any questions or feedback, please contact us at: [yasarserkan016@gmail.com](mailto:yasarserkan016@gmail.com)
