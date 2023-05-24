import React from 'react'
import CircleProgress from '../../../app/common/components/CircleProgress'
import { Container, Divider, Grid, GridColumn, GridRow, Header, Icon, List, Modal, Tab, Table } from 'semantic-ui-react'
import { tr } from 'date-fns/locale';
import { FutureExpenditure } from '../../../app/models/spendingPlan/futureExpenditure';
import exp from 'constants';

interface Props {
  expenditure: FutureExpenditure
}

function ExpenditureDetails({expenditure}: Props) {
  return (
    <Container fluid>
      <Header as={'h2'} content='Expenditure Details' textAlign='center' />
      <Divider style={{marginBottom: '30px'}} />
      <Grid columns={2}>
      <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Category:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${expenditure.category}`} />
          </Grid.Column>
        </Grid.Row>        
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Real Amount:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${expenditure.completedAmount}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Budgeted Amount:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${expenditure.amount}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Account:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${expenditure.accountName}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <GridColumn width={8}>
            <Header as={'h3'} content='Progress:' />
          </GridColumn>
          <Grid.Column width={8}>
            <CircleProgress progress={expenditure.completedAmount / expenditure.amount} />
          </Grid.Column>
        </Grid.Row>
      </Grid>
      <Container fluid style={{height: '200px', width: '100%', overflow: 'auto'}}>
        <Table celled fixed style={{borderRadius: '25px', width: '100%'}}>
          <Table.Header style={{position: 'sticky', top: '0'}}>
            <Table.Row>
              <Table.HeaderCell>Title</Table.HeaderCell>
              <Table.HeaderCell>Date</Table.HeaderCell>
              <Table.HeaderCell>Amount</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {expenditure.completedExpenditures.map((transaction, index) => (
              <Table.Row key={index}>
                <Table.Cell>{transaction.title}</Table.Cell>
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

export default ExpenditureDetails