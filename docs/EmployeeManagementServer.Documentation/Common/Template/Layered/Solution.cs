namespace EmployeeManagementServer.Documentation.Common.Template.Layered;

public class Solution
{
    #region Introduction

    /*
     * This template provides a layered application structure based on the Domain Driven Design (DDD) practices. This document explains the solution structure and projects in details.
     */
    
    /* Default Structure
     * If you don't specify any additional options, you will have a solution as shown below:
     * Projects are organized in docs, src and test folders. src folder contains the actual application which is layered based on DDD principles as mentioned before. The notes below shows the layers & project dependencies of the application:
     * 1. *.Application
     * 2. *.Application.Contracts
     * 3. *.Domain
     * 4. *.Domain.Shared
     * 5. *.Infrastructure (Services Project that contains both .EntityFrameworkCore Project and .DbMigrator Projects)
     * 6. *.HttpApi
     *
     * Each section below will explain the related project & its dependencies.
     */

    #region .Domain.Shared Project

    /*
     * This project contains constants, enums and other objects these are actually a part of the domain layer, but needed to be used by all layers/projects in the solution.
     *
     * A BookType enum and a BookConsts class (which may have some constant fields for the Book entity, like MaxNameLength) are good candidates for this project.
     *
     * This project has no dependency on other projects in the solution. All other projects depend on this one directly or indirectly.
     */

    #endregion

    #region .Domain Project

    /*
     * This is the domain layer of the solution. It mainly contains entities, aggregate roots, domain services, value objects, repository interfaces and other domain objects.
     *
     * A Book entity, a BookManager domain service and an IBookRepository interface are good candidates for this project.
     *
     * Depends on the .Domain.Shared because it uses constants, enums and other objects defined in that project.
     */

    #endregion

    #region .Application.Contracts Project

    /*
     * This project mainly contains application service interfaces and Data Transfer Objects (DTO) of the application layer. It exists to separate the interface & implementation of the application layer. In this way, the interface project can be shared to the clients (if it will be need for working vai terminal) as a contract package.
     *
     * An IBookAppService interface and a BookCreationDto class are good candidates for this project.
     *
     * Depends on the .Domain.Shared because it may use constants, enums and other shared objects of this project in the application service interfaces and DTOs.
     */

    #endregion

    #region .Application Project

    /*
     * This project contains the application service implementations of the interfaces defined in the .Application.Contracts project.
     *
     * A BookAppService class is a good candidate for this project.
     *
     * Depends on the .Application.Contracts project to be able to implement the interfaces and use the DTOs.
     *
     * Depends on the .Domain project to be able to use domain objects (entities, repository interfaces... etc.) to perform the application logic.
     */

    #endregion

    #region .Infrastructure Project

    /*
     * Also known as Service Project that contains both .EntityFrameworkCore Project and .DbMigrator Projects.
     */
    
    /* .EntityFrameworkCore Project
     * This is the integration project for the EF Core. It defines the DbContext and implements repository interfaces defined in the .Domain project.
     *
     * Depends on the .Domain project to be able to reference to entities and repository interfaces.
     * 
     * This project is available only if you are using EF Core as the database provider. If you select another database provider, its name will be different.
     */

    /* .DbMigrator Project
     * This is a console application that simplifies the execution of database migrations on development and production environments. When you run this application, it:
     * 1. Creates the database if necessary.
     * 2. Applies the pending database migrations.
     * 3. Seeds initial data if needed.
     *
     * This project has its own appsettings.json file. So, if you want to change the database connection string, remember to change this file too.
     *
     * While creating database & applying migrations seem only necessary for relational databases, this project comes even if you choose a NoSQL database provider (like MongoDB). In that case, it still seeds the initial data which is necessary for the application.
     *
     * Depends on the .EntityFrameworkCore project (for EF Core) since it needs to access to the migrations.
     * Depends on the .Application.Contracts project to be able to access permission definitions, because the initial data seeder grants all permissions to the admin role by default.
     */
    
    #endregion

    #region .HttpApi Project

    /*
     * This project is used to define your API Controllers.
     *
     * Depends on the .Application.Contracts project to be able to inject the application service interfaces.
     *
     * This project is an application that hosts the API of the solution. It has its own appsettings.json that contains database connection and other configurations.
     */

    #endregion
    
    #endregion
}