import React, { useState } from "react";
import { Container, Grid } from "semantic-ui-react";

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

  const toggleGoalDetails = (goalId: number): void => {
    const updatedGoals = goals.map((goal) => {
      if (goal.id === goalId) {
        return { ...goal, showDetails: !goal.showDetails };
      }
      return goal;
    });
    setGoals(updatedGoals);
  };

  return (
    <Container>
      <div style={{ fontSize: "50px", textAlign: "center", marginTop: "50px" }}>
        Goals
      </div>
      <Grid columns={2} doubling stackable centered style={{ marginTop: "50px" }}>
        {goals.map((goal) => (
          <Grid.Column key={goal.id}>
            <div
              style={{ fontSize: "20px", textAlign: "center", cursor: "pointer" }}
              onClick={() => toggleGoalDetails(goal.id)}
            >
              {goal.title} {goal.showDetails ? "(-)" : "(+)"}
            </div>
            {goal.showDetails && (
              <div style={{ marginTop: "20px", textAlign: "center" }}>
                <ul style={{ listStyle: "none", padding: 0 }}>
                  <li>{goal.description}</li>
                  <li>{goal.currentAmount}</li>
                  <li>{goal.requiredAmount}</li>
                  <li>{goal.endDate}</li>
                </ul>
              </div>
            )}
          </Grid.Column>
        ))}
      </Grid>
    </Container>
  );
};

export default GoalsPage;