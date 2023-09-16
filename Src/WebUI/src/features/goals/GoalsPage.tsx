import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Container, Grid, Card, List, Divider, Header } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';



const mockGoals = [
  {
    id: 1,
    name: 'Test 1',
    description: 'Test 1'
  },
  {
    id: 2,
    name: 'Test 2',
    description: 'Test 2'
  },
  {
    id: 3,
    name: 'Test 3',
    description: 'Test 3'
  },
];

const GoalsPage = () => {
  const cardStyle = {
    margin: '20px auto',
    padding: '20px',
    border: '1px solid #ccc',
    borderRadius: '5px',
    textAlign: 'center',
    boxShadow: '0px 0px 7px 4px rgba(0, 0, 0, 0.25)',
  };

  const centeredContentStyle = {
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    height: '100%',
  };

  return (
    <Container>
      <Grid centered>
        <Grid.Row>
          <Grid.Column width={8} textAlign='center'>
            <h1>Goals</h1>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={8}>
            <Card.Group centered itemsPerRow={1} stackable>
              {mockGoals.map((goal, index) => (
                <Card key={goal.id} style={cardStyle}>
                  <Card.Content style={centeredContentStyle}>
                    <Header as='h2'>{goal.name}</Header>
                    <p>{goal.description}</p>
                  </Card.Content>
                </Card>
              ))}
            </Card.Group>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </Container>
  );
};

export default GoalsPage;