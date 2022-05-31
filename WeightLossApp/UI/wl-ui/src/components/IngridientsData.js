import React,{Component} from 'react';
import { constants } from '../Constants';

export class IngridientsData extends Component {

    constructor(props) {
        super(props);

        this.state = {
            ingridientsData:[]
        }
    }

    refreshList() {
        fetch(constants.API_URL + 'IngridientData')
        .then(response=>response.json())
        .then(data=> {
            this.setState({ingridientsData:data})
        });
    }

    componentDidMount() {
        this.refreshList();
    }


    render() {

        const {
            ingridientsData
        } = this.state;

        return (
            <div>
                <h3 className='m-5'>This is Ingridients page</h3>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Calories</th>
                            <th>Proteins</th>
                            <th>Carbohydrates</th>
                            <th>Fats</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {ingridientsData.map(item => 
                            <tr>
                                <td>{item.ID}</td>
                                <td>{item.Name}</td>
                                <td>{item.Calories}</td>
                                <td>{item.Proteins}</td>
                                <td>{item.Carbohydrates}</td>
                                <td>{item.Fats}</td>
                                <td>
                                    <button type='button' className='btn btn-dark mx-2'>Edit</button>
                                    <button type='button' className='btn btn-danger mx-2'>Delete</button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
                
        )
    }
}