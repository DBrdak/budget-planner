document.addEventListener("DOMContentLoaded", function () {
  var mockSpendings = [
    {
      category: "Groceries",
      amount: 50.25,
      date: "2023-05-01",
    },
    {
      category: "Entertainment",
      amount: 20.0,
      date: "2023-05-05",
    },
    {
      category: "Transportation",
      amount: 15.75,
      date: "2023-05-08",
    },
    {
      category: "Dining Out",
      amount: 40.5,
      date: "2023-05-12",
    },
    {
      category: "Shopping",
      amount: 80.6,
      date: "2023-05-15",
    },
    {
      category: "Utilities",
      amount: 65.0,
      date: "2023-05-18",
    },
    {
      category: "Healthcare",
      amount: 35.2,
      date: "2023-05-22",
    },
    {
      category: "Travel",
      amount: 120.0,
      date: "2023-05-25",
    },
    {
      category: "Education",
      amount: 50.0,
      date: "2023-05-29",
    },
  ];

  var spendingsBody = document.getElementById("spendings-body");

  spendingsBody.innerHTML = "";

  mockSpendings.forEach(function (spending) {
    var row = document.createElement("tr");

    var categoryCell = document.createElement("td");
    categoryCell.textContent = spending.category;
    row.appendChild(categoryCell);

    var amountCell = document.createElement("td");
    amountCell.textContent = "$" + spending.amount.toFixed(2);
    row.appendChild(amountCell);

    var dateCell = document.createElement("td");
    dateCell.textContent = spending.date;
    row.appendChild(dateCell);

    spendingsBody.appendChild(row);
  });
  document.addEventListener("DOMContentLoaded", function () {
    var spendingsBody = document.getElementById("spendings-body");
    var spendingForm = document.getElementById("spending-form");
    var categoryInput = document.getElementById("category");
    var amountInput = document.getElementById("amount");

    function addSpending(category, amount) {
      var row = document.createElement("tr");

      var categoryCell = document.createElement("td");
      categoryCell.textContent = category;
      row.appendChild(categoryCell);

      var amountCell = document.createElement("td");
      amountCell.textContent = "$" + amount.toFixed(2);
      row.appendChild(amountCell);

      var dateCell = document.createElement("td");
      var currentDate = new Date();
      dateCell.textContent = currentDate.toISOString().split("T")[0];
      row.appendChild(dateCell);

      spendingsBody.appendChild(row);
    }

    function handleFormSubmit(event) {
      event.preventDefault();

      var category = categoryInput.value;
      var amount = parseFloat(amountInput.value);

      if (category.trim() === "" || isNaN(amount) || amount <= 0) {
        alert("Please enter a valid category and amount.");
        return;
      }

      spendings.push({ category: category, amount: amount });

      addSpending(category, amount);

      categoryInput.value = "";
      amountInput.value = "";
    }

    spendingForm.addEventListener("submit", handleFormSubmit);

    categoryInput.addEventListener("input", function () {
      var category = categoryInput.value;
      var amount = parseFloat(amountInput.value);

      spendingsBody.innerHTML = "";

      spendings.forEach(function (spending) {
        addSpending(spending.category, spending.amount);
      });

      if (category.trim() !== "" && !isNaN(amount) && amount > 0) {
        addSpending(category, amount);
      }
    });

    amountInput.addEventListener("input", function () {
      var category = categoryInput.value;
      var amount = parseFloat(amountInput.value);

      spendingsBody.innerHTML = "";

      spendings.forEach(function (spending) {
        addSpending(spending.category, spending.amount);
      });

      if (category.trim() !== "" && !isNaN(amount) && amount > 0) {
        addSpending(category, amount);
      }
    });
  });
});
