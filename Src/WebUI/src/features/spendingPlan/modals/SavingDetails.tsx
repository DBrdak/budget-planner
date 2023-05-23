import React from 'react'
import CircleProgress from '../../../app/common/components/CircleProgress'
import { Container, Divider, Grid, GridColumn, GridRow, Header, Icon, List, Modal, Tab, Table } from 'semantic-ui-react'
import { tr } from 'date-fns/locale';

interface Props {
  saving: any
}

function SavingDetails({saving}: Props) {
  const transactions = [
      {
        id: 1,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 2,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 3,
        name: 'Bułko',
        amount: 1488
      },
      {
        id: 4,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 5,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 6,
        name: 'Bułko',
        amount: 1488
      },
      {
        id: 7,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 8,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 9,
        name: 'Bułko',
        amount: 1488
      },
      {
        id: 10,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 11,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 12,
        name: 'Bułko',
        amount: 1488
      },
      {
        id: 13,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 14,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 15,
        name: 'Bułko',
        amount: 1488
      },
      {
        id: 16,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 17,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 18,
        name: 'Bułko',
        amount: 1488
      },
      {
        id: 19,
        name: 'Ziólko',
        amount: 2147
      },
      {
        id: 20,
        name: 'Kółko',
        amount: 420
      },
      {
        id: 21,
        name: 'Bułko',
        amount: 1488
      }
    ];
  return (
    <Container fluid>
      <Header as={'h2'} content='Saving Details' textAlign='center' />
      <Divider />
      <Grid columns={2}>
      <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Category:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving}`} />
          </Grid.Column>
        </Grid.Row>        
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Real Amount:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Budgeted Amount:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <Grid.Column width={8}>
            <Header as={'h3'} content='Account:' />
          </Grid.Column>
          <Grid.Column width={8}>
            <Header as={'h4'} content= {`${saving}`} />
          </Grid.Column>
        </Grid.Row>
        <Grid.Row style={{marginBottom: '10px'}}>
          <GridColumn width={8}>
            <Header as={'h3'} content='Progress:' />
          </GridColumn>
          <Grid.Column width={8}>
            <CircleProgress progress={30} />
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
            {transactions.map((transaction, index) => (
              <Table.Row key={index}>
                <Table.Cell>{transaction.name}</Table.Cell>
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