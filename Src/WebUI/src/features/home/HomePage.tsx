import { observer } from 'mobx-react-lite'
import React from 'react'
import { Link } from 'react-router-dom'
import { Button, Container } from 'semantic-ui-react'
import { useStore } from '../../app/stores/store'

function HomePage() {
  const {commonStore} = useStore()
  commonStore.setToken("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImpvaG4iLCJuYW1laWQiOiIwNjdiZDU5Zi1iYjhjLTQ0NjUtOTg2ZS05MDlmNzZjNDA1ZjIiLCJlbWFpbCI6ImpvaG5AdGVzdC5jb20iLCJuYmYiOjE2ODQ5NjAxNDEsImV4cCI6MTY4NTA0NjU0MSwiaWF0IjoxNjg0OTYwMTQxfQ.GZH5EoTJqytXKEihPlifJzLKhfR2tR-heQHpfYR3AdiHXSHoaijM4ZtIaORy_M0Ek3Yy1Q1JPfaLaeBUDeclKg")
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