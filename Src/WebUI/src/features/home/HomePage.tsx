import { observer } from 'mobx-react-lite'
import React from 'react'
import { Link } from 'react-router-dom'
import { Button, Container } from 'semantic-ui-react'

function HomePage() {
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