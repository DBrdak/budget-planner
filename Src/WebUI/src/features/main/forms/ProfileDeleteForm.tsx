import { Form, Formik } from 'formik'
import React from 'react'
import { useStore } from '../../../app/stores/store'
import { Button, Container, Divider, Header } from 'semantic-ui-react'
import MyTextInput from '../../../app/common/forms/MyTextInput'
import { observer } from 'mobx-react-lite'
import ProfileForm from './ProfileForm'

function ProfileDeleteForm() {
  const {modalStore, profileStore} = useStore()
  const {profile, deleteProfile} = profileStore

  function handleProfileDelete(password: string) {
    deleteProfile(password)
  }

  

  function handleUndo(): void {
    modalStore.closeModal()
    modalStore.openModal(<ProfileForm username={profile.username}/>)
  }

  return (
    <Container style={{display: 'flex', textAlign: 'center', verticalAlign: 'middle'}}>
      <Formik 
      initialValues={{password: ''}}
      onSubmit={(values, {setErrors}) => handleProfileDelete(values.password)}>
        {({handleSubmit, isSubmitting, errors}) => (
          <Form 
          className='ui form' 
          onSubmit={handleSubmit}
          autoComplete='off'>
            <Header as='h1' content='Insert your password below' />
            <Divider />
            <Header as='h3' content='Keep in mind - this action cannot be undone!' color='red' />
            <MyTextInput placeholder='' name='password' type='password' />
            <Container textAlign='center'>
              <Button primary negative content="I'm sure" type='submit' />
              <Button primary positive content="I've changed my mind" onClick={() => handleUndo()} />
            </Container>
          </Form>
        )}
      </Formik>
    </Container>
  )
}

export default observer(ProfileDeleteForm)