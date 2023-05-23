import { observer } from 'mobx-react-lite'
import { useState } from 'react'
import { Container, Header, Icon } from 'semantic-ui-react'
import MonthMenu from './menus/MonthMenu'
import YearMenu from './menus/YearMenu'
import IncomeTable from './tables/IncomeTable'
import ExpenditureTable from './tables/ExpenditureTable'
import SavingTable from './tables/SavingTable'
import { Link } from 'react-router-dom'
import { da } from 'date-fns/locale'

function SpendingPlan() {
  const [selectedDate, setSelectedDate] = useState(new Date())

  const containerStyle = {
    boxSizing: "border-box",
    width: "100%",
    height: "100%",
    display: "flex",
    flexDirection: "column",
    justifyContent: "flex-start",
    alignItems: "center",
    padding: "35px 35px 35px 35px",
    overflow: "auto",
    alignContent: "center",
    flexWrap: "nowrap",
    gap: 40,
    position: "absolute",
    borderRadius: "0px 0px 0px 0px",
  }

  const incomes = [
    {
      category: 'Salary',
      account: 'Bank Account',
      realAmount: 2000,
      budgetedAmount: 5000,
      completion: 100
    },
    {
      category: 'Freelance',
      account: 'PayPal',
      realAmount: 120,
      budgetedAmount: 2000,
      completion: 75
    },
    {
      category: 'Investment',
      account: 'Investment Account',
      realAmount: 3000,
      budgetedAmount: 2500,
      completion: 120
    }
  ];

  const savings = [
    {
      goal: "Vacation",
      fromAccount: "Savings",
      toAccount: "Travel",
      realAmount: 5000,
      budgetedAmount: 6000,
    },
    {
      goal: "Home Renovation",
      fromAccount: "Checking",
      toAccount: "Home Improvement",
      realAmount: 10000,
      budgetedAmount: 12000,
    },
    {
      goal: "Education",
      fromAccount: "Investments",
      toAccount: "College Fund",
      realAmount: 20000,
      budgetedAmount: 25000,
    },
  ];

  const handleMonthChange = (monthIndex: number) => {
    selectedDate.setUTCMonth(monthIndex)
    console.log(selectedDate)
  }

  const handleYearChange = (year: number) => {
    selectedDate.setUTCFullYear(year)
    console.log(selectedDate)
  }

  return (
    <Container fluid style={containerStyle}>
      <Link to="/main">
        <Icon name="home" color="grey" size="big" />
      </Link>
      <Header as={'h1'} content='Spending Plan' color='green'
      style={{fontSize: '48px'}} />
      <YearMenu onChange={(year) => handleYearChange(year)}/>
      <MonthMenu date={selectedDate} onChange={(monthIndex) => handleMonthChange(monthIndex)}/>
      <IncomeTable incomes={incomes} />
      <ExpenditureTable expenditures={incomes} />
      <SavingTable savings={savings} />
    </Container>
  )
}

export default observer(SpendingPlan)