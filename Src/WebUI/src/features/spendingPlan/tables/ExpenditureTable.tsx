import { observer } from 'mobx-react-lite';
import React, { useState } from 'react';
import { Button, Grid, GridColumn, Header, Icon, Label, Popup, Progress, Segment, Table } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';
import ExpenditureDetails from '../modals/ExpenditureDetails';
import IncomeDetails from '../modals/IncomeDetails';
import { FutureExpenditure } from '../../../app/models/spendingPlan/futureExpenditure';
import FutureExpenditureAdd from '../forms/FutureExpenditureAdd';

interface ExpenditureTableProps {
  date: Date
}

const ExpenditureTable: React.FC<ExpenditureTableProps> = ({ date }) => {
  const {modalStore, spendingPlanStore} = useStore()
  const {expenditures, loading, loadingInitial, deleteExpenditure} = spendingPlanStore
  const [tableVisible, setTableVisible] = useState(false)

  const handleToggleTable = () => {
    setTableVisible(!tableVisible)
  }

  const handleDelete = (expenditure: FutureExpenditure) => {
     deleteExpenditure(expenditure)
  }

  return (
    <div style={{width: '100%'}}>
      <Segment loading={loadingInitial} style={{ borderRadius: '25px', width: '100%', display: 'flex', alignItems: 'center' }} onClick={handleToggleTable}>
        <Grid style={{width: '100%', margin: '0px', display: 'flex', alignItems: 'center'}}>
          <GridColumn textAlign='center' width={3}>
            <Header as={'h2'} color='red' content='Expenditures' style={{margin: '0px'}}/>
          </GridColumn>       
          <GridColumn textAlign='center' width={2}>
            <Label color='red' content={`${expenditures.length}`} style={{margin: '0px'}}/>
          </GridColumn>
          <GridColumn textAlign='center' width={8}>
            <Progress color='red' style={{ margin: '0px'}}
            value={expenditures.reduce((sum, expenditure) => (
              sum += (expenditure.completedAmount / expenditure.amount) > 1 ? 1 : expenditure.completedAmount / expenditure.amount
              ), 0)}
            total={expenditures.length}/>
          </GridColumn>        
          <GridColumn textAlign='center' width={3}>
            {tableVisible ? 
            <Icon name='caret square up' color='red' size='big' onClick={handleToggleTable} style={{ cursor: 'pointer', margin: '0px'}}/> : 
            <Icon name='caret square down' color='red' size='big' onClick={handleToggleTable} style={{ cursor: 'pointer', margin: '0px'}}/>}
          </GridColumn>
        </Grid>
      </Segment>
      {tableVisible &&
        <Table celled style={{borderRadius: '25px'}}>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell />
            <Table.HeaderCell>Category</Table.HeaderCell>
            <Table.HeaderCell>Account</Table.HeaderCell>
            <Table.HeaderCell>Real Amount</Table.HeaderCell>
            <Table.HeaderCell>Budgeted Amount</Table.HeaderCell>
            <Table.HeaderCell>Progress</Table.HeaderCell>
            <Table.HeaderCell textAlign='center'>
            <Button icon='add' color='green' inverted size='big' circular
              onClick={() => modalStore.openModal(<FutureExpenditureAdd date={date}/>)}/>
            </Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {expenditures.map((expenditure, index) => (
              <Table.Row key={index}>
                <Table.Cell width={1} verticalAlign='middle' textAlign='center'>
                  <Button icon='eye' inverted secondary circular size='big'
                  onClick={() => modalStore.openModal(<ExpenditureDetails expenditure={expenditure} />)}/>
                </Table.Cell>
                <Table.Cell>{expenditure.category}</Table.Cell>
                <Table.Cell>{expenditure.accountName}</Table.Cell>
                <Table.Cell>{expenditure.completedAmount}</Table.Cell>
                <Table.Cell>{expenditure.amount}</Table.Cell>
                <Table.Cell>
                  <Progress value={expenditure.completedAmount} 
                  total={expenditure.amount}
                  color='red'/>
                </Table.Cell>
                  <Table.Cell verticalAlign='middle' textAlign='center'>
                    <Button icon='trash' inverted color='red' size='big' circular
                    onClick={(e) => handleDelete(expenditure)} loading={loading}/>
                </Table.Cell>
              </Table.Row>
          ))}
        </Table.Body>
      </Table>}
    </div>
  );
};

export default observer(ExpenditureTable);

