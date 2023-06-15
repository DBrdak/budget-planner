import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Container, Grid, Card } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';

const GoalsPage = () => {
  const { goalsStore } = useStore();

  useEffect(() => {
    goalsStore.getGoals();
  }, [goalsStore]);

  return (
    <Container>
      <Grid centered>
        <Grid.Row>
          <Grid.Column width={16} textAlign='center'>
              <h1>Goals</h1>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row>
          <Grid.Column width={16}>
            <Card.Group>
            {goalsStore.goals.map((goal) => (
                  <Card key={goal.id}>
                    <Card.Content>
                      <Card.Header>{goal.name}</Card.Header>
                      <Card.Description>{goal.description}</Card.Description>
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

export default observer(GoalsPage);
