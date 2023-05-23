import React, { useState } from 'react';
import { Button, Grid, GridColumn, Header, Icon, Progress, Segment, Table } from 'semantic-ui-react';
import SavingDetails from '../modals/SavingDetails';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';
import { Link } from 'react-router-dom';

interface Saving {
  goal: string;
  fromAccount: string;
  toAccount: string;
  realAmount: number;
  budgetedAmount: number;
}

interface SavingTableProps {
  savings: Saving[];
}

const SavingTable: React.FC<SavingTableProps> = ({ savings }) => {
  const {modalStore} = useStore()
  const [tableVisible, setTableVisible] = useState(false)

  const handleToggleTable = () => {
    setTableVisible(!tableVisible)
  }
  return (
    <div style={{width: '100%'}}>
      <Segment style={{ borderRadius: '25px', width: '100%', display: 'flex', alignItems: 'center'}} onClick={handleToggleTable}>
        <Grid style={{width: '100%', margin: '0px', display: 'flex', alignItems: 'center'}}>
          <GridColumn textAlign='center' width={4}>
            <Header as={'h2'} color='blue' content='Savings' style={{margin: '0px'}}/>
          </GridColumn>        
          <GridColumn textAlign='center' width={8}>
            <Progress color='blue' style={{ margin: '0px'}}
            value={savings.reduce((sum, saving) => sum + saving.realAmount, 0) / savings.reduce((sum, saving) => sum + saving.budgetedAmount, 0)}
            total={savings.length}/>
          </GridColumn>
          <GridColumn textAlign='center' width={4}>
            {tableVisible ? 
            <Icon name='caret square up' color='blue' size='big' onClick={handleToggleTable} style={{ cursor: 'pointer', margin: '0px'}}/> : 
            <Icon name='caret square down' color='blue' size='big' onClick={handleToggleTable} style={{ cursor: 'pointer', margin: '0px'}}/>}
          </GridColumn>
        </Grid>
      </Segment>
      {tableVisible &&
        <Table celled style={{borderRadius: '25px'}}>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>Goal</Table.HeaderCell>
            <Table.HeaderCell>From Account</Table.HeaderCell>
            <Table.HeaderCell>To Account</Table.HeaderCell>
            <Table.HeaderCell>Real Amount</Table.HeaderCell>
            <Table.HeaderCell>Budgeted Amount</Table.HeaderCell>
            <Table.HeaderCell>Progress</Table.HeaderCell>
            <Table.HeaderCell textAlign='center'>
            <Icon name='add' color='green' size='big' 
                  style={{ cursor: 'pointer'}} onClick={() => console.log(savings)}/>
            </Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {savings.map((saving, index) => (
            <Table.Row key={index} onClick={() => modalStore.openModal(<SavingDetails saving={saving} />)}
            style={{cursor: 'pointer'}}>
              <Table.Cell>{saving.goal}</Table.Cell>
              <Table.Cell>{saving.fromAccount}</Table.Cell>
              <Table.Cell>{saving.toAccount}</Table.Cell>
              <Table.Cell>{saving.realAmount}</Table.Cell>
              <Table.Cell>{saving.budgetedAmount}</Table.Cell>
              <Table.Cell>
                <Progress value={saving.realAmount} 
                total={saving.budgetedAmount} color='blue'/>
              </Table.Cell>
              <Table.Cell verticalAlign='middle' textAlign='center'>
                <Icon name='trash' color='blue' size='big' 
                style={{ cursor: 'pointer'}} onClick={() => console.log(saving)}/>
              </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>}
    </div>
  );
};

export default observer(SavingTable);

