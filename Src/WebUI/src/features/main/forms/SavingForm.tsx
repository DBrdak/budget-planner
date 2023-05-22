import { Formik, Form } from 'formik'
import { useNavigate } from 'react-router-dom'
import { Button, Container, DropdownItemProps, Header } from 'semantic-ui-react';
import MyTextInput from '../../../app/common/forms/MyTextInput';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';
import MyDropdown from '../../../app/common/forms/MyDropdown';
import MyDateInput from '../../../app/common/forms/MyDateInput';

function SavingForm() {
  const {modalStore} = useStore()

  // To będzie do wywalenia ->
  const navigate = useNavigate();

  const submit = (values: any) => {
    modalStore.closeModal()
    console.log(values)
    navigate('/main', { replace: true });
  };

  const categories: DropdownItemProps[] = [
    { text: 'Option 1', value: 'option1' },
    { text: 'Option 2', value: 'option2' },
  ]
  // <-

  return (
    <Formik
    initialValues={{goal: '', amount: '', 
    fromAccount: '', toAccount: '', date: ''}}
    onSubmit={(values, {setErrors}) => submit(values)}>
      {({handleSubmit, isSubmitting, errors}) => (
        <Form 
        className='ui form' 
        onSubmit={handleSubmit}
        autoComplete='off'>
          <Header as={'h1'} content='New Saving' textAlign='center' />
          <MyTextInput placeholder='Amount' name='amount' />
          <MyDropdown placeholder='Goal' name='goal' options={categories}  />
          <MyDropdown placeholder='From Account' name='fromAccount' options={categories} />
          <MyDropdown placeholder='To Account' name='toAccount' options={categories} />
          <MyDateInput 
          placeholderText='Date'
          name='date' 
          dateFormat='d MMMM, yyyy' />
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

export default observer(SavingForm)