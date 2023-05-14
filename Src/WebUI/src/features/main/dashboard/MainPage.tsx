import React from 'react';
import { Grid, Card, Icon, Segment, Container, Button, CardHeader, Header, Image } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';

const MainPage = () => {
  return (
    <Container fluid style={{marginTop:'12px'}}>
      <Grid centered verticalAlign='middle' style={{ height: '100vh'}}>
        <Grid.Row columns={3} verticalAlign='bottom'>
          <Grid.Column width={4}>
          <Button 
          fluid color='green' size='massive' 
          style={{alignItems: 'center', borderRadius: '50px' }}>
            <Header inverted>Accounts</Header>
            <div style={{ marginTop: 'auto' }}>
              <Icon name='university' size='big'/>
            </div>
          </Button>
          </Grid.Column>
          <Grid.Column width={4} textAlign='center'>
            <Button 
            fluid color='green' size='massive' 
            style={{alignItems: 'center', borderRadius: '50px' }}>
              <Header inverted>Goals</Header>
              <div style={{ marginTop: 'auto' }}>
                <Icon name='bullseye' size='big'/>
              </div>
            </Button>
          </Grid.Column>
          <Grid.Column width={4} textAlign='center'>
            <Button 
            fluid color='green' size='massive' 
            style={{alignItems: 'center', borderRadius: '50px' }}>
              <Header inverted>Profile</Header>
              <div style={{ marginTop: 'auto' }}>
                <Icon name='user' size='big'/>
              </div>
            </Button>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={4} textAlign='center'>
            <Button 
            fluid size='massive' color='green'
            style={{alignItems: 'center', borderRadius: '50px' }}>
              <Header inverted>Spending Plan</Header>
              <div style={{ marginTop: 'auto' }}>
                <Icon name='calculator' size='big'/>
              </div>
            </Button>
          </Grid.Column>
          <Grid.Column width={4} textAlign='center' >
            <Button.Group fluid vertical>
              <Button 
              color='green' size='mini' 
              style={{alignItems: 'center', borderRadius:'30px', justifyContent:'center', textAlign:'center'}}>
                Income
                <div style={{ marginTop: 'auto', marginLeft:'5px'}}>
                  <Icon name='plus' size='big'/>
                </div>
              </Button>
              <Button 
              color='green' size='mini' 
              style={{alignItems: 'center', margin:'3px 0px 3px 0px', borderRadius:'30px', justifyContent:'center', textAlign:'center' }}>
                Expenditure
                <div style={{ marginTop: 'auto', marginLeft:'5px' }}>
                  <Icon name='minus' size='big'/>
                </div>
              </Button>
              <Button 
              color='green' size='mini' 
              style={{alignItems: 'center', borderRadius:'30px', justifyContent:'center', textAlign:'center'}}>
                Saving
                <div style={{ marginTop: 'auto', marginLeft:'5px' }}>
                  <Icon name='shield' size='big'/>
                </div>
              </Button>
            </Button.Group>
          </Grid.Column>
          <Grid.Column width={4}>
            <Image src='/Assets/Logo.png'/>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </Container>
  );
};

export default MainPage;