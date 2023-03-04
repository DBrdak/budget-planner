CREATE TABLE "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL
);


CREATE TABLE "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "DisplayName" TEXT NULL,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL
);


CREATE TABLE "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);


CREATE TABLE "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);


CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);


CREATE TABLE "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);


CREATE TABLE "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);


CREATE TABLE "Budgets" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Budgets" PRIMARY KEY,
    "Name" TEXT NULL,
    "UserId" TEXT NULL,
    CONSTRAINT "FK_Budgets_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);


CREATE TABLE "Accounts" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Accounts" PRIMARY KEY,
    "Name" TEXT NULL,
    "AccountType" TEXT NULL,
    "Balance" REAL NOT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_Accounts_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE
);


CREATE TABLE "Goals" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Goals" PRIMARY KEY,
    "Name" TEXT NULL,
    "Description" TEXT NULL,
    "EndDate" Date NOT NULL,
    "CurrentAmount" REAL NOT NULL,
    "RequiredAmount" REAL NOT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_Goals_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE
);


CREATE TABLE "TransactionCategories" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_TransactionCategories" PRIMARY KEY,
    "Value" TEXT NULL,
    "Type" TEXT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_TransactionCategories_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE
);


CREATE TABLE "FutureTransactions" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_FutureTransactions" PRIMARY KEY,
    "Category" TEXT NULL,
    "Amount" REAL NOT NULL,
    "CompletedAmount" REAL NOT NULL,
    "Date" Date NOT NULL,
    "AccountId" TEXT NOT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_FutureTransactions_Accounts_AccountId" FOREIGN KEY ("AccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FutureTransactions_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE
);


CREATE TABLE "FutureSavings" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_FutureSavings" PRIMARY KEY,
    "Amount" REAL NOT NULL,
    "CompletedAmount" REAL NOT NULL,
    "Date" Date NOT NULL,
    "GoalId" TEXT NULL,
    "FromAccountId" TEXT NOT NULL,
    "ToAccountId" TEXT NOT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_FutureSavings_Accounts_FromAccountId" FOREIGN KEY ("FromAccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FutureSavings_Accounts_ToAccountId" FOREIGN KEY ("ToAccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FutureSavings_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FutureSavings_Goals_GoalId" FOREIGN KEY ("GoalId") REFERENCES "Goals" ("Id")
);


CREATE TABLE "Transactions" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Transactions" PRIMARY KEY,
    "Amount" REAL NOT NULL,
    "Title" TEXT NULL,
    "Date" Date NOT NULL,
    "Category" TEXT NULL,
    "AccountId" TEXT NOT NULL,
    "FutureTransactionId" TEXT NOT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_Transactions_Accounts_AccountId" FOREIGN KEY ("AccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Transactions_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Transactions_FutureTransactions_FutureTransactionId" FOREIGN KEY ("FutureTransactionId") REFERENCES "FutureTransactions" ("Id") ON DELETE CASCADE
);


CREATE TABLE "Savings" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Savings" PRIMARY KEY,
    "Amount" REAL NOT NULL,
    "Date" Date NOT NULL,
    "FromAccountId" TEXT NOT NULL,
    "ToAccountId" TEXT NOT NULL,
    "GoalId" TEXT NOT NULL,
    "FutureSavingId" TEXT NOT NULL,
    "BudgetId" TEXT NOT NULL,
    CONSTRAINT "FK_Savings_Accounts_FromAccountId" FOREIGN KEY ("FromAccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Savings_Accounts_ToAccountId" FOREIGN KEY ("ToAccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Savings_Budgets_BudgetId" FOREIGN KEY ("BudgetId") REFERENCES "Budgets" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Savings_FutureSavings_FutureSavingId" FOREIGN KEY ("FutureSavingId") REFERENCES "FutureSavings" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Savings_Goals_GoalId" FOREIGN KEY ("GoalId") REFERENCES "Goals" ("Id") ON DELETE RESTRICT
);


CREATE INDEX "IX_Accounts_BudgetId" ON "Accounts" ("BudgetId");


CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");


CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");


CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");


CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");


CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");


CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");


CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");


CREATE UNIQUE INDEX "IX_Budgets_UserId" ON "Budgets" ("UserId");


CREATE INDEX "IX_FutureSavings_BudgetId" ON "FutureSavings" ("BudgetId");


CREATE INDEX "IX_FutureSavings_FromAccountId" ON "FutureSavings" ("FromAccountId");


CREATE INDEX "IX_FutureSavings_GoalId" ON "FutureSavings" ("GoalId");


CREATE INDEX "IX_FutureSavings_ToAccountId" ON "FutureSavings" ("ToAccountId");


CREATE INDEX "IX_FutureTransactions_AccountId" ON "FutureTransactions" ("AccountId");


CREATE INDEX "IX_FutureTransactions_BudgetId" ON "FutureTransactions" ("BudgetId");


CREATE INDEX "IX_Goals_BudgetId" ON "Goals" ("BudgetId");


CREATE INDEX "IX_Savings_BudgetId" ON "Savings" ("BudgetId");


CREATE INDEX "IX_Savings_FromAccountId" ON "Savings" ("FromAccountId");


CREATE INDEX "IX_Savings_FutureSavingId" ON "Savings" ("FutureSavingId");


CREATE INDEX "IX_Savings_GoalId" ON "Savings" ("GoalId");


CREATE INDEX "IX_Savings_ToAccountId" ON "Savings" ("ToAccountId");


CREATE INDEX "IX_TransactionCategories_BudgetId" ON "TransactionCategories" ("BudgetId");


CREATE INDEX "IX_Transactions_AccountId" ON "Transactions" ("AccountId");


CREATE INDEX "IX_Transactions_BudgetId" ON "Transactions" ("BudgetId");


CREATE INDEX "IX_Transactions_FutureTransactionId" ON "Transactions" ("FutureTransactionId");


