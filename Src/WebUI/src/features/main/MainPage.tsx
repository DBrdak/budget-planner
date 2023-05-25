import React from 'react';
import { Grid, Card, Icon, Segment, Container, Button, CardHeader, Header, Image } from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import '../../app/layout/styles.css';
import { useStore } from '../../app/stores/store';
import SavingForm from './forms/SavingForm';
import ProfileForm from './forms/ProfileForm';
import IncomeForm from './forms/IncomeForm';
import ExpenditureForm from './forms/ExpenditureForm';

function MainPage() {
  const {modalStore} = useStore()

  const btnStyle = {
    alignItems: 'center', 
    borderRadius: '50px', 
    height: '350px', 
    flexShrink: 0,
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    boxShadow: "0px 0px 7px 4px rgba(0, 0, 0, 0.25)",
    position: "relative",
    justifySelf: "start",
    alignSelf: "start",
    padding: "0px 0px 0px 0px",
    alignContent: "center",
    flexWrap: "nowrap",
    gap: 8
  }

  return (
    <Container fluid style={{marginTop: '12px'}} >
      <Grid  centered verticalAlign='middle' stackable textAlign='center' style={{height: '100vh'}}>
        <Grid.Row columns={3} verticalAlign='bottom'>
          <Grid.Column width={5} >
          <Button
          style={btnStyle}
          fluid color='green' size='massive'>
            <div>
              <Header inverted as={'h1'}>Accounts</Header>
              <Icon name='university' size='big'/>
            </div>
          </Button>
          </Grid.Column>
          <Grid.Column width={5} textAlign='center' >
            <Button
            as={Link} to={'/goals'}
            fluid color='green' size='massive' 
            style={btnStyle}>
              <div>
                <Header inverted as={'h1'}>Goals</Header>
                <Icon name='bullseye' size='big'/>
              </div>
            </Button>
          </Grid.Column>
          <Grid.Column width={5} textAlign='center'>
            <Button as={Link}
            onClick={() => modalStore.openModal(<ProfileForm />)}
            fluid color='green' size='massive' 
            style={btnStyle}>
              <div>
                <Header inverted as={'h1'}>Profile</Header>
                <Icon name='user' size='big'/>
              </div>
            </Button>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row verticalAlign='top' >
          <Grid.Column width={5} textAlign='center'>
            <Button 
            as={Link} to={'/spendingplan'}
            fluid size='massive' color='green'
            style={btnStyle}>
              <div>
                <Header inverted as={'h1'}>Spending Plan</Header>
                <Icon name='calculator' size='big'/>
              </div>
            </Button>
          </Grid.Column>
          <Grid.Column width={5} textAlign='center' >
            <Button.Group fluid vertical style={{ height: '350px'}}>
              <Button 
              color='green' size='mini' as={Link}
              onClick={() => modalStore.openModal(<IncomeForm />)}
              style={{alignItems: 'center', borderRadius:'30px', justifyContent:'center'}}>
                <div>
                  <Header inverted as={'h1'}>Income</Header>
                  <Icon name='plus' size='big'/>
                </div>
              </Button>
              <Button 
              color='green' size='mini' as={Link}
              onClick={() => modalStore.openModal(<ExpenditureForm />)}
              style={{alignItems: 'center', margin:'12px 0px 12px 0px', borderRadius:'30px', justifyContent:'center' }}>
                <div>
                  <Header inverted as={'h1'}>Expenditure</Header>
                  <Icon name='minus' size='big'/>
                </div>
              </Button>
              <Button 
              color='green' size='mini' as={Link}
              onClick={() => modalStore.openModal(<SavingForm />)}
              style={{alignItems: 'center', borderRadius:'30px', justifyContent:'center' }}>
                <div>
                  <Header inverted as={'h1'}>Saving</Header>
                  <Icon name='shield' size='big'/>
                </div>
              </Button>
            </Button.Group>
          </Grid.Column>
          <Grid.Column width={5}>
            <Image src='/Assets/Logo.png'/>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </Container>
  );
};

export default observer(MainPage);