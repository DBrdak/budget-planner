import React, { useState } from 'react';
import { Button, Grid, GridColumn, Header, Icon, Label, Progress, Segment, Table } from 'semantic-ui-react';
import SavingDetails from '../modals/SavingDetails';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';
import { Link } from 'react-router-dom';
import FutureSavingAdd from '../forms/FutureSavingAdd';
import { FutureSaving } from '../../../app/models/spendingPlan/futureSaving';

interface SavingTableProps {
  date: Date
}

const SavingTable: React.FC<SavingTableProps> = ({ date }) => {
  const {modalStore,spendingPlanStore} = useStore()
  const {savings, loading, loadingInitial, deleteSaving} = spendingPlanStore
  const [tableVisible, setTableVisible] = useState(false)

  const handleToggleTable = () => {
    setTableVisible(!tableVisible)
  }

  const handleDelete = (saving: FutureSaving) => {
    deleteSaving(saving)
  }

  return (
    <div style={{width: '100%'}}>
      <Segment loading={loadingInitial} style={{ borderRadius: '25px', width: '100%', display: 'flex', alignItems: 'center'}} onClick={handleToggleTable}>
        <Grid style={{width: '100%', margin: '0px', display: 'flex', alignItems: 'center'}}>
          <GridColumn textAlign='center' width={3}>
            <Header as={'h2'} color='blue' content='Savings' style={{margin: '0px'}}/>
          </GridColumn>
          <GridColumn textAlign='center' width={2}>
            <Label color='blue' content={`${savings.length}`} style={{margin: '0px'}}/>
          </GridColumn>    
          <GridColumn textAlign='center' width={8}>
            <Progress color='blue' style={{ margin: '0px'}} 
            value={savings.reduce((sum, saving) => (
              sum += (saving.completedAmount / saving.amount) > 1 ? 1 : saving.completedAmount / saving.amount
            ), 0)}
            total={savings.length}/>
          </GridColumn>
          <GridColumn textAlign='center' width={3}>
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
            <Table.HeaderCell />
            <Table.HeaderCell>Goal</Table.HeaderCell>
            <Table.HeaderCell>From Account</Table.HeaderCell>
            <Table.HeaderCell>To Account</Table.HeaderCell>
            <Table.HeaderCell>Real Amount</Table.HeaderCell>
            <Table.HeaderCell>Budgeted Amount</Table.HeaderCell>
            <Table.HeaderCell>Progress</Table.HeaderCell>
            <Table.HeaderCell textAlign='center'>
            <Button icon='add' color='green' inverted size='big' circular
            onClick={() => modalStore.openModal(<FutureSavingAdd date={date}/>)}/>
            </Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          {savings.map((saving, index) => (
            <Table.Row key={index}>
              <Table.Cell width={1} verticalAlign='middle' textAlign='center'>
                <Button icon='eye' inverted secondary circular size='big'
                onClick={() => modalStore.openModal(<SavingDetails saving={saving} />)}/>
              </Table.Cell>
              <Table.Cell>{saving.goalName}</Table.Cell>
              <Table.Cell>{saving.fromAccountName}</Table.Cell>
              <Table.Cell>{saving.toAccountName}</Table.Cell>
              <Table.Cell>{saving.completedAmount}</Table.Cell>
              <Table.Cell>{saving.amount}</Table.Cell>
              <Table.Cell>
                <Progress value={saving.completedAmount} 
                total={saving.amount} color='blue'/>
              </Table.Cell>
              <Table.Cell verticalAlign='middle' textAlign='center'>
                <Button icon='trash' inverted color='red' size='big' circular
                onClick={() => handleDelete(saving)} loading={loading}/>
              </Table.Cell>
            </Table.Row>
          ))}
        </Table.Body>
      </Table>}
    </div>
  );
};

export default observer(SavingTable);

