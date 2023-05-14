import React from "react";
import { Container, Grid } from "semantic-ui-react";

const GoalsPage = () => {
    return (
        <Container>
            <div style={{ fontSize: "50px", textAlign: "center", marginTop: "50px" }}>
                Goals
            </div>
            <Grid columns={2} doubling stackable centered style={{ marginTop: "50px" }}>
                <Grid.Column>
                    <div style={{ fontSize: "20px", textAlign: "center" }}>
                        Column 1 
                    </div>
                </Grid.Column>
                <Grid.Column>
                    <div style={{ fontSize: "20px", textAlign: "center" }}>
                        Column 2 
                    </div>
                </Grid.Column>
            </Grid>
        </Container>
    );
};

export default GoalsPage;