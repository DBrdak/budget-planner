import { Dimmer, Loader } from 'semantic-ui-react'

interface Props {
  inverted?: boolean,
  content?: string,
}

function LoadingComponent({inverted = true, content='Loading...'}: Props) {
  return (
    <Dimmer active={true} inverted={inverted} style={{height: '100px'}}>
      <Loader content={content} style={{height: '200px'}}/>
    </Dimmer>
  )
}

export default LoadingComponent