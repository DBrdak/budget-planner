import { observer } from 'mobx-react-lite'
import { useEffect, useState } from 'react'
import { Container, Header, Icon } from 'semantic-ui-react'
import MonthMenu from './menus/MonthMenu'
import YearMenu from './menus/YearMenu'
import IncomeTable from './tables/IncomeTable'
import ExpenditureTable from './tables/ExpenditureTable'
import SavingTable from './tables/SavingTable'
import { Link } from 'react-router-dom'
import { da } from 'date-fns/locale'
import { useStore } from '../../app/stores/store'
import { FutureExpenditure } from '../../app/models/spendingPlan/futureExpenditure'

function SpendingPlan() {
  const {spendingPlanStore} = useStore();
  const {loadExpenditures, loadIncomes, loadSavings, clearAll, expenditureRegistry, incomeRegistry, savingRegistry} = spendingPlanStore;
  const [selectedDate, setSelectedDate] = useState(new Date())

  useEffect(() => {
    if(expenditureRegistry.size <= 1) loadExpenditures(new Date(selectedDate))
    if(incomeRegistry.size <= 1) loadIncomes(new Date(selectedDate))
    if(savingRegistry.size <= 1) loadSavings(new Date(selectedDate))
  },[loadSavings, loadIncomes, loadExpenditures, 
    expenditureRegistry.size, incomeRegistry.size, savingRegistry.size ])

  const loadAll = () => {
    loadIncomes(new Date(selectedDate))
    loadExpenditures(new Date(selectedDate))
    loadSavings(new Date(selectedDate))
  }

  const containerStyle = {
    boxSizing: "border-box",
    width: "100%",
    height: "100%",
    display: "flex",
    flexDirection: "column",
    justifyContent: "flex-start",
    alignItems: "center",
    padding: "35px 125px 35px 125px",
    overflow: "auto",
    alignContent: "center",
    flexWrap: "nowrap",
    gap: 40,
    position: "absolute",
    borderRadius: "0px 0px 0px 0px",
  }

  const handleMonthChange = (monthIndex: number) => {
    selectedDate.setUTCMonth(monthIndex)
    clearAll()
    loadAll()
  }

  const handleYearChange = (year: number) => {
    selectedDate.setUTCFullYear(year)
    clearAll()
    loadAll()
  }

  return (
    <Container style={containerStyle}>
      <Link to="/main">
        <Icon name="home" color="grey" size="big" />
      </Link>
      <Header as={'h1'} content='Spending Plan' color='green'
      style={{fontSize: '48px'}} />
      <YearMenu onChange={(year) => handleYearChange(year)}/>
      <MonthMenu date={selectedDate} onChange={(monthIndex) => handleMonthChange(monthIndex)}/>
      <IncomeTable date={selectedDate} />
      <ExpenditureTable date={selectedDate} />
      <SavingTable date={selectedDate} />
    </Container>
  )
}

export default observer(SpendingPlan)