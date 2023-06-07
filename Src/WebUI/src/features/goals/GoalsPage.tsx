import axios from 'axios';
import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, Container, Grid } from 'semantic-ui-react';

const GoalsPage = () => {
  const [goals, setGoals] = useState([]);

  useEffect(() => {
    const fetchGoals = async() => {
      try {
        const response = await axios.get('/goals')
        setGoals(response.data.goals);
      } catch (error) {
        console.error('Error fetching goals', error);
      }
    };
    fetchGoals();
  }, []);
    

  const toggleGoalDetails = (goalId: number) => {
    const updatedGoals = goals.map((goal) =>
      goal.id === goalId ? { ...goal, showDetails: !goal.showDetails } : goal
    );
    setGoals(updatedGoals);
  };

  return (
    <Container>
      <div style={{ fontSize: '50px', textAlign: 'center', marginTop: '50px' }}>
        Goals
      </div>
      {goals.length === 0 ? (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>No goals found</div>
      ) : (
        <Grid centered style={{ marginTop: '50px' }}>
          <Grid.Row columns={1} doubling stackable>
            {goals.map((goal) => (
              <Grid.Column key={goal.id} style={{ textAlign: 'center' }}>
                <div style={{ marginBottom: '20px' }}>
                  <Card
                    style={{
                      width: '400px',
                      margin: '0 auto',
                      border: goal.showDetails ? '2px solid green' : 'none',
                    }}
                    onMouseEnter={() => toggleGoalDetails(goal.id)}
                    onMouseLeave={() => toggleGoalDetails(goal.id)}
                  >
                    <Card.Content>
                      <Card.Header style={{ fontSize: '20px', cursor: 'pointer' }}>
                        {goal.title}
                      </Card.Header>
                      <Card.Description
                        style={{
                          marginTop: '20px',
                          display: goal.showDetails ? 'block' : 'none',
                        }}
                      >
                        <ul style={{ listStyle: 'none', padding: 0 }}>
                          <li>{goal.currentAmount}</li>
                          <li>{goal.requiredAmount}</li>
                          <li>{goal.endDate}</li>
                          <li>{goal.description}</li>
                        </ul>
                      </Card.Description>
                    </Card.Content>
                  </Card>
                </div>
              </Grid.Column>
            ))}
          </Grid.Row>
        </Grid>
      )}
    </Container>
  );
};

export default observer (GoalsPage);

