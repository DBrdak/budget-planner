import React from 'react'
import CircleProgress from '../../../app/common/components/CircleProgress'
import { Container, Divider, Grid, GridColumn, GridRow, Header, Icon, List, Modal, Tab, Table } from 'semantic-ui-react'
import { tr } from 'date-fns/locale';
import { FutureSaving } from '../../../app/models/spendingPlan/futureSaving';

interface Props {
  saving: FutureSaving
}

function SavingDetails({saving}: Props) {
  return (
    <Container fluid>
      <Header as={'h2'} content='Saving Details' textAlign='center' />
      <Divider style={{marginBottom: '30px'}} />
      <Grid columns={2}>
      <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Goal:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving.goalName}`} />
          </Grid.Column>
        </Grid.Row>        
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Real Amount:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving.completedAmount}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Budgeted Amount:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving.amount}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content=' From Account:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving.fromAccountName}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content=' To Account:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving.toAccountName}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <GridColumn width={8}>
            <Header as={'h3'} content='Progress:' />
          </GridColumn>
          <Grid.Column width={8}>
            <CircleProgress progress={saving.completedAmount / saving.amount} />
          </Grid.Column>
        </Grid.Row>
      </Grid>
      <Container fluid style={{height: '200px', width: '100%', overflow: 'auto'}}>
        <Table celled fixed style={{borderRadius: '25px', width: '100%'}}>
          <Table.Header style={{position: 'sticky', top: '0'}}>
            <Table.Row>
              <Table.HeaderCell>Date</Table.HeaderCell>
              <Table.HeaderCell>Amount</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {saving.completedSavings.map((transaction, index) => (
              <Table.Row key={index}>
                <Table.Cell>{new Date(transaction.date).toDateString()}</Table.Cell>
                <Table.Cell>{transaction.amount}</Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table>
      </Container>
    </Container>
  )
}

export default SavingDetails