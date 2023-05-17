import React, { useState } from 'react';
import { Card, Container, Grid } from 'semantic-ui-react';

const GoalsPage = () => {
  const [goals, setGoals] = useState([
    {
      id: 1,
      title: "Goal 1",
      description: "Description 1",
      currentAmount: "Current Amount 1",
      requiredAmount: "Required Amount 1",
      endDate: "End Date 1",
      showDetails: false,
    },
    {
      id: 2,
      title: "Goal 2",
      description: "Description 2",
      currentAmount: "Current Amount 2",
      requiredAmount: "Required Amount 2",
      endDate: "End Date 2",
      showDetails: false,
    },
  ]);

  const toggleGoalDetails = (goalId: number) => {
    const updatedGoals = goals.map((goal) =>
      goal.id === goalId ? { ...goal, showDetails: !goal.showDetails } : goal
    );
    setGoals(updatedGoals);
  };

  return (
    <Container>
      <div style={{ fontSize: "50px", textAlign: "center", marginTop: "50px" }}>
        Goals
      </div>
      <Grid centered style={{ marginTop: "50px" }}>
        <Grid.Row columns={2} doubling stackable>
          {goals.map((goal) => (
            <Grid.Column key={goal.id} style={{ textAlign: "center" }}>
              <div style={{ marginBottom: "20px" }}>
                <Card
                  style={{
                    width: "400px",
                    margin: "0 auto",
                    border: goal.showDetails ? "2px solid green" : "none",
                  }}
                  onMouseEnter={() => toggleGoalDetails(goal.id)}
                  onMouseLeave={() => toggleGoalDetails(goal.id)}
                >
                  <Card.Content>
                    <Card.Header
                      style={{ fontSize: "20px", cursor: "pointer" }}
                    >
                      {goal.title}
                    </Card.Header>
                    <Card.Description
                      style={{ marginTop: "20px", display: goal.showDetails ? "block" : "none" }}
                    >
                      <ul style={{ listStyle: "none", padding: 0 }}>
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
    </Container>
  );
};

export default GoalsPage;
