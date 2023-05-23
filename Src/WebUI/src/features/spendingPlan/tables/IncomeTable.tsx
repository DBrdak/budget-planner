import React, { useState } from 'react';
import { Button, Grid, GridColumn, Header, Icon, Progress, Segment, Table } from 'semantic-ui-react';
import IncomeDetails from '../modals/IncomeDetails';
import modalStore from '../../../app/stores/modalStore';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';

interface Income {
  category: string;
  account: string;
  realAmount: number;
  budgetedAmount: number;
}

interface IncomeTableProps {
  incomes: Income[];
}

const IncomeTable: React.FC<IncomeTableProps> = ({ incomes }) => {
  const {modalStore} = useStore()
  const [tableVisible, setTableVisible] = useState(false)

  const handleToggleTable = () => {
    setTableVisible(!tableVisible)
  }
  return (
    <div style={{width: '100%'}}>
      <Segment style={{ borderRadius: '25px', width: '100%', display: 'flex', alignItems: 'center' }} onClick={handleToggleTable}>
        <Grid style={{width: '100%', margin: '0px', display: 'flex', alignItems: 'center'}}>
          <GridColumn textAlign='center' width={4}>
            <Header as={'h2'} color='green' content='Incomes' style={{margin: '0px'}}/>
          </GridColumn>        
          <GridColumn textAlign='center' width={8}>
            <Progress color='green' style={{ margin: '0px'}}
            value={incomes.reduce((sum, income) => sum + income.realAmount, 0)}
            total={incomes.reduce((sum, income) => sum + income.budgetedAmount, 0)}/>
          </GridColumn>        
          <GridColumn textAlign='center' width={4}>
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
            <Table.HeaderCell>Category</Table.HeaderCell>
            <Table.HeaderCell>Account</Table.HeaderCell>
            <Table.HeaderCell>Real Amount</Table.HeaderCell>
            <Table.HeaderCell>Budgeted Amount</Table.HeaderCell>
            <Table.HeaderCell>Progress</Table.HeaderCell>
            <Table.HeaderCell textAlign='center'>
            <Icon name='add' color='green' size='big' 
                  style={{ cursor: 'pointer'}} onClick={() => console.log(incomes)}/>
            </Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {incomes.map((income, index) => (
            <Table.Row key={index} 
            onClick={() => modalStore.openModal(<IncomeDetails income={income} />)}
            style={{cursor: 'pointer'}}>
              <Table.Cell>{income.category}</Table.Cell>
              <Table.Cell>{income.account}</Table.Cell>
              <Table.Cell>{income.realAmount}</Table.Cell>
              <Table.Cell>{income.budgetedAmount}</Table.Cell>
              <Table.Cell>
                <Progress value={income.realAmount} 
                total={income.budgetedAmount} 
                color='green'/>
                </Table.Cell>
                <Table.Cell verticalAlign='middle' textAlign='center'>
                  <Icon name='trash' color='red' size='big' 
                  style={{ cursor: 'pointer'}} onClick={() => console.log(income)}/>
                </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>}
    </div>
  );
};

export default observer(IncomeTable);

