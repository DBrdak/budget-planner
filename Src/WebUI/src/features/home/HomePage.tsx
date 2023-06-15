import { observer } from 'mobx-react-lite'
import React from 'react'
import { Link } from 'react-router-dom'
import { Button, Container } from 'semantic-ui-react'
import { useStore } from '../../app/stores/store'

function HomePage() {
  const {commonStore} = useStore()
  commonStore.setToken("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImpvaG4iLCJuYW1laWQiOiI5ZmRhMTk2Ny1kZGI0LTRmMTktYWJjNS00OWM5YjVjZWViYmIiLCJlbWFpbCI6ImpvaG5AdGVzdC5jb20iLCJuYmYiOjE2ODYxNDgwNjQsImV4cCI6MTY4NjIzNDQ2NCwiaWF0IjoxNjg2MTQ4MDY0fQ.4t07xn_xqo_28qVMJ44TedjoPoNBPaMSjvK2UfLUm6nIKp13E1WDzBWwk04I0JF5mppVD9pzkeXlP6JBstu1Qg")
  commonStore.setBudgetName('JohnnyBudget')

  return (
    <Container 
    style={{ height: '100vh', 
    display: 'flex', alignItems: 'center', 
    justifyContent: 'center' }}>
    <Button 
      as={Link} to='/main'
      circular
      color='green'
      content='Go to main' />
    </Container>
  )
}

export default observer(HomePage)