import { useState } from 'react';
import { Menu } from 'semantic-ui-react';

interface Props {
  onChange: (year: number) => void
}

const YearMenu = ({onChange}: Props) => {
  const years = [
    new Date().getUTCFullYear() - 1,
    new Date().getUTCFullYear(),
    new Date().getUTCFullYear() + 1,
  ];

  const [activeYear, setActiveYear] = useState(years[1]);

  const handleItemClick = (year: number) => {
    setActiveYear(year)
    onChange(year)
  };

  return (
    <Menu pagination borderless style={{textAlign: 'center', border: 'none', boxShadow: 'none', width: '500px', height: '25px', justifyContent: 'center'}}>
      <Menu.Menu style={{width: '400px', justifyContent: 'center'}} >
        {years.map((year, index) => (
          <Menu.Item
            key={index}
            name={year.toString()}
            active={years[index] === activeYear}
            onClick={() => handleItemClick(years[index])}
            color='green'
            style={{ fontSize: years[index] === activeYear ? '25px' : '15px', width: '33%', justifyContent: 'center', verticalAlign: 'middle'}}
          />
        ))}
      </Menu.Menu>
    </Menu>
  );
};

export default YearMenu;
