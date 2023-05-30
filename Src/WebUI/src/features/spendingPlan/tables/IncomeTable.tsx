import React, { useState } from 'react';
import { Button, Grid, GridColumn, Header, Icon, Label, Progress, Segment, Table } from 'semantic-ui-react';
import IncomeDetails from '../modals/IncomeDetails';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { FutureIncome } from '../../../app/models/spendingPlan/futureIncome';
import FutureIncomeAdd from '../forms/FutureIncomeAdd';

interface IncomeTableProps {
  date: Date
}

const IncomeTable: React.FC<IncomeTableProps> = ({ date }) => {
  const {modalStore, spendingPlanStore} = useStore()
  const {incomes, loading, loadingInitial, deleteIncome} = spendingPlanStore
  const [tableVisible, setTableVisible] = useState(false)

  const handleToggleTable = () => {
    setTableVisible(!tableVisible)
  } 

  const handleDelete = (income: FutureIncome) => {
    deleteIncome(income)
  }

  return (
    <div style={{width: '100%'}}>
      <Segment loading={loadingInitial} style={{ borderRadius: '25px', width: '100%', display: 'flex', alignItems: 'center' }} onClick={handleToggleTable}>
        <Grid style={{width: '100%', margin: '0px', display: 'flex', alignItems: 'center'}}>
          <GridColumn textAlign='center' width={3}>
            <Header as={'h2'} color='green' content='Incomes' style={{margin: '0px'}}/>
          </GridColumn>
          <GridColumn textAlign='center' width={2}>
            <Label color='green' content={`${incomes.length}`} style={{margin: '0px'}}/>
          </GridColumn>       
          <GridColumn textAlign='center' width={8}>
            <Progress color='green' style={{ margin: '0px'}}
            value={incomes.reduce((sum, income) => (
              sum += (income.completedAmount / income.amount) > 1 ? 1 : income.completedAmount / income.amount
              ), 0)}
            total={incomes.length}/>
          </GridColumn>        
          <GridColumn textAlign='center' width={3}>
            {tableVisible ? 
            <Icon name='caret square up' color='green' size='big' onClick={handleToggleTable} style={{ cursor: 'pointer', margin: '0px'}}/> : 
            <Icon name='caret square down' color='green' size='big' onClick={handleToggleTable} style={{ cursor: 'pointer', margin: '0px'}}/>}
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
            onClick={() => modalStore.openModal(<FutureIncomeAdd date={date}/>)}/>
            </Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {incomes.map((income, index) => (
            <Table.Row key={index}>
                <Table.Cell width={1} verticalAlign='middle' textAlign='center'>
                  <Button icon='eye' inverted secondary circular size='big'
                  onClick={() => modalStore.openModal(<IncomeDetails income={income} />)}/>
                </Table.Cell>
              <Table.Cell>{income.category}</Table.Cell>
              <Table.Cell>{income.accountName}</Table.Cell>
              <Table.Cell>{income.completedAmount}</Table.Cell>
              <Table.Cell>{income.amount}</Table.Cell>
              <Table.Cell>
                <Progress value={income.completedAmount} 
                total={income.amount} 
                color='green'/>
                </Table.Cell>
                <Table.Cell verticalAlign='middle' textAlign='center'>
                  <Button icon='trash' inverted color='red' size='big' circular 
                  onClick={() => handleDelete(income)} loading={loading}/>
                </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>}
    </div>
  );
};

export default observer(IncomeTable);

