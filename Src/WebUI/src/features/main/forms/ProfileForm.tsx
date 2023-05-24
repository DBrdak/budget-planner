import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import React from 'react'
import { Button, Container, Divider, DropdownItemProps, Form, Grid, Header } from 'semantic-ui-react'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import MyDropdown from '../../../app/common/forms/MyDropdown'
import MyDateInput from '../../../app/common/forms/MyDateInput'
import { useNavigate } from 'react-router-dom'
import { useStore } from '../../../app/stores/store'

function ProfileForm() {  
  const {modalStore} = useStore()

  // To będzie do wywalenia ->

  const submit = (values: any) => {
    modalStore.closeModal()
  };

  const categories: DropdownItemProps[] = [
    { text: 'Option 1', value: 'option1' },
    { text: 'Option 2', value: 'option2' },
]
// <-
  return (
    <Formik
    initialValues={{email: '', username: '', 
    displayName: '', budgetName: '', newPassword: '', oldPassword: ''}}
    onSubmit={(values, {setErrors}) => submit(values)}>
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as='h1' content='Profile Settings' textAlign='center' />
          <Divider />
          <Grid >
            <Grid.Column width={8} textAlign='center'>
              <Header as='h4' content='Update your data' />
              <MyTextInput placeholder='Email' name='email' />
              <MyTextInput placeholder='Username' name='username' />
              <MyTextInput placeholder='Display name' name='displayName' />
              <MyTextInput placeholder='Budget name' name='budgetName' />
              <Button primary positive content='Accept' />
            </Grid.Column>
            <Divider vertical/>       
            <Grid.Column width={8} textAlign='center' >
              <Header as='h4' content='Change password' />
              <MyTextInput placeholder='New Password' name='newPassword' type='password'/>
              <MyTextInput placeholder='Old Password' name='oldPassword' type='password'/>
              <Button primary positive content='Accept' />
            </Grid.Column>
          </Grid>
          <Divider />
          <Container textAlign='center' style={{marginTop: '15px'}}>
            <Button primary negative content='Logout' icon='logout' />
          </Container>
        </Form>
      )}
    </Formik>
  )
}

export default observer(ProfileForm)