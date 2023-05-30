import { observer } from 'mobx-react-lite'
import React from 'react'
import { Link } from 'react-router-dom'
import { Button, Container } from 'semantic-ui-react'
import { useStore } from '../../app/stores/store'

function HomePage() {
  const {commonStore} = useStore()
  commonStore.setToken("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImpvaG5ueSIsIm5hbWVpZCI6IjA2N2JkNTlmLWJiOGMtNDQ2NS05ODZlLTkwOWY3NmM0MDVmMiIsImVtYWlsIjoiam9obkB0ZXN0LmNvbSIsIm5iZiI6MTY4NTQ4NjM5NCwiZXhwIjoxNjg1NTcyNzk0LCJpYXQiOjE2ODU0ODYzOTR9.Sw1gZWIr7HBoVo3zK9WZtSFOAO3cuqPdtZZBDYuunSpQRlSO569zLMUiIkwhvH9wF06Q2hZNtafzwe0QcakzZw")
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