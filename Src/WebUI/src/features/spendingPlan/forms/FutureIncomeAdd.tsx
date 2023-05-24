import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React from 'react'
import { Form } from 'react-router-dom'
import { Header, Divider, Container, Button, DropdownItemProps } from 'semantic-ui-react'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import { useStore } from '../../../app/stores/store'

interface Props {
  date: Date
}

function FutureIncomeAdd({date}: Props) {
  const {modalStore, spendingPlanStore} = useStore()
  const {createExpenditure} = spendingPlanStore

  return (
    <Formik
    initialValues={{amount: '', 
    category: '', accountName: '', date: ''}}
    onSubmit={(values, {setErrors}) => 
      null} >
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Future Income' textAlign='center' />
          <Divider />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyTextInput placeholder='Category' name='category' />
          <MyDropdown placeholder='Account' name='account' options={[{name:'Gold'}]} />
          <Container
          style={{ display: 'flex', justifyContent: 'space-between', width: '60%' }}>
            <Button loading={isSubmitting} icon='check' positive type='submit' circular />
            <Button loading={isSubmitting} icon='close' negative type='submit' circular />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(FutureIncomeAdd)