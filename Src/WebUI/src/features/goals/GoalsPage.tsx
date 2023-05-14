import React, { useState } from "react";
import { Container, Grid } from "semantic-ui-react";

const GoalsPage = () => {
  const [showGoal1Details, setShowGoal1Details] = useState(false);
  const [showGoal2Details, setShowGoal2Details] = useState(false);

  const toggleGoal1Details = () => {
    setShowGoal1Details(!showGoal1Details);
    setShowGoal2Details(false);
  };

  const toggleGoal2Details = () => {
    setShowGoal2Details(!showGoal2Details);
    setShowGoal1Details(false);
  };

  return (
    <Container>
      <div style={{ fontSize: "50px", textAlign: "center", marginTop: "50px" }}>
        Goals
      </div>
      <Grid columns={2} doubling stackable centered style={{ marginTop: "50px" }}>
        <Grid.Column>
          <div
            style={{ fontSize: "20px", textAlign: "center", cursor: "pointer" }}
            onClick={toggleGoal1Details}
          >
            Goal 1 {showGoal1Details ? "(-)" : "(+)"}
          </div>
          {showGoal1Details && (
            <ul style={{ marginTop: "20px", textAlign: "center" }}>
              <li>Description</li>
              <li>Current Amount</li>
              <li>Required Amount</li>
              <li>End Date</li>
            </ul>
          )}
        </Grid.Column>
        <Grid.Column>
          <div
            style={{ fontSize: "20px", textAlign: "center", cursor: "pointer" }}
            onClick={toggleGoal2Details}
          >
            Goal 2 {showGoal2Details ? "(-)" : "(+)"}
          </div>
          {showGoal2Details && (
            <ul style={{ marginTop: "20px", textAlign: "center" }}>
              <li>Description</li>
              <li>Current Amount</li>
              <li>Required Amount</li>
              <li>End Date</li>
            </ul>
          )}
        </Grid.Column>
      </Grid>
    </Container>
  );
};

export default GoalsPage;
