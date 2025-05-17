Okay, this looks like a solid WPF project for library management. Let's break it down into a more structured and comprehensive description, suitable for a portfolio or README.

---

## Projet Gestion Bibliothèque WPF (WPF Library Management Project)

**Créé Par / Created By:** ID LAHCEN El Mahdi & KHALIL El Houssine

### Project Overview:
This project is a Windows Presentation Foundation (WPF) application designed to manage the operations of a library. It provides a user-friendly interface for managing books, members, categories, and borrowing records, along with a dashboard for a quick overview of library statistics.

---

### Key Features:

1.  **Dashboard (Tableau de Bord):**
    *   Serves as the application's homepage.
    *   Displays key statistics:
        *   Total number of Books
        *   Total number of Members (Adhérents)
        *   Total number of Categories
        *   Number of Borrowed Books (Emprunts)
    *   Provides a quick visual summary of the library's status.
    ![Dashboard][image1]

2.  **Book Management (Gestion des Livres):**
    *   **Add Book (Ajout d'un livre):**
        *   Form for entering book details: Titre (Title), Auteur (Author), ISBN, Nombre de pages (Number of Pages), Catégorie (Category - likely a dropdown), Date de Publication (Publication Date - likely a date picker).
        *   Success confirmation message upon adding a book.
        ![Add Book Success][image2]
    *   **List Books (Liste des livres ajoutées):**
        *   Displays a comprehensive list of all books in a data grid.
        *   Columns include: Titre, Auteur, ISBN, Nb Page, Catégorie, Date Publication.
        *   Provides options to Edit and Delete each book.
        ![Book List][image3]
    *   **Sorting (Fonction du Sorting):**
        *   Allows users to organize the book list by clicking on column headers (e.g., sorting by "Titre").
        ![Sorting Books][image4]
    *   **Searching (Fonction du Recherche):**
        *   Enables users to search for books based on various attributes like Titre, Auteur, ISBN, Catégorie.
        ![Search Books][image5]
    *   **Edit Book (Modification d'un livre):**
        *   Allows modification of existing book details through a pre-filled form.
        ![Edit Book][image6]
    *   **Delete Book (Suppression d'un livre):**
        *   Provides a confirmation dialog before deleting a book to prevent accidental deletions.
        ![Delete Book][image7]

3.  **Member Management (Gestion des Adhérents):**
    *   Includes the same core functionalities as Book Management:
        *   Add New Member
        *   List Members
        *   Edit Member Details
        *   Delete Member
        *   Search and Sort Members
    *   Likely includes fields such as: Name, Address, Contact Information, Membership ID, etc. (though fields are not explicitly shown in this screenshot).
    ![Member Management][image8]

4.  **Borrowing Management (Gestion des Emprunts):**
    *   **Record New Borrowing:**
        *   Form to select the Livre (Book) and Adhérent (Member) - likely using dropdowns.
        *   Input fields for Date Emprunt (Borrow Date) and Date Retour Prévue (Expected Return Date).
    *   **List Borrowed Books:**
        *   Displays a list of all current and past borrowings.
        *   Likely includes details like Book Title, Member Name, Borrow Date, Expected Return Date, Actual Return Date, and Status (Borrowed/Returned).
        *   Options to manage returns.
    ![Borrowing Management][image9]

5.  **Category Management (Gestion des Catégories):**
    *   **Add New Category:**
        *   Simple form to add a new book category by name (Nom).
    *   **List Categories:**
        *   Displays existing categories.
        *   Provides options to Edit and Delete categories.
    ![Category Management][image10]

---

### Technical Details:

*   **Platform:** Windows Desktop
*   **Framework:** Windows Presentation Foundation (WPF)
*   **Language:** Likely C# (standard for WPF development)
*   **User Interface:** XAML
*   **Database Connectivity:**
    *   The application uses an `appsettings.json` file to store database connection information. This is standard practice for .NET Core / .NET 5+ applications, suggesting the project might be built on one of these newer .NET versions or is adopting this configuration pattern.
    *   Users need to modify this file with their specific database server details (e.g., server name, database name, username, password) to connect the application to their database.
    *   Likely uses an Object-Relational Mapper (ORM) such as Entity Framework Core for database interactions.
    ![appsettings.json for DB Connection][image11]

---

### Setup and Configuration:

To run this project, you would typically need:
1.  Microsoft Visual Studio (e.g., 2019 or later).
2.  .NET SDK (compatible with the project's target version).
3.  A database server (e.g., SQL Server, MySQL, PostgreSQL - depending on what the application is configured for).

**Steps:**
1.  Clone or download the project source code.
2.  Open the project in Visual Studio.
3.  **Configure the Database Connection:**
    *   Locate the `appsettings.json` file in the project.
    *   Update the `ConnectionString` section with the appropriate details for your database server. For example, for SQL Server, it might look like:
        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD;Trusted_Connection=False;Encrypt=False;"
          },
          // ... other settings
        }
        ```
4.  Ensure the necessary database (and its schema/tables) is set up. If using Entity Framework migrations, you might need to run `Update-Database` from the Package Manager Console.
5.  Build and run the application from Visual Studio.
   
---

### Conclusion:
The "Projet Gestion Bibliothèque WPF" is a well-rounded desktop application demonstrating proficiency in WPF, C#, and database management. It covers essential library functions with a clear and intuitive user interface, making it a practical tool for managing library resources.

---

 ### Screenshots:
 
