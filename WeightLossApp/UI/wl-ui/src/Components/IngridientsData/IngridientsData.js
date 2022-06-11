import React, { Component } from "react";
import { constants } from "../../Constants";
import { MultiSelect } from "react-multi-select-component";
import { Button, Table} from "react-bootstrap";

// Component for IngridientData page
export class IngridientsData extends Component {
	// Main constructor
	constructor(props) {
		super(props);

		// State initialization
		// Component state - properties of this component
		this.state = {
			// List of data to be displayed
			ingridientsData: [],
			categories: [],
			// Title of the modal window
			modalTitle: "",
			// Data of the selected item
			// This data will be displayed in the modal window
			itemID: 0,
			itemName: "",
			itemCalories: 0,
			itemProteins: 0,
			itemCarbohydrates: 0,
			itemFats: 0,
			itemCategories: [],
			// Options for multiselect
			selectOptions: [],
			// Data to display as multiselect selected
			categoryNames: [], 
		};
	}

	// Called on Add Button click
	addClick() {
		// Clearing item data and saving current state
		this.setState({
			modalTitle: "Adding new Ingridient",
			itemID: 0,
			itemName: "",
			itemCalories: 0,
			itemProteins: 0,
			itemCarbohydrates: 0,
			itemFats: 0,
			itemCategories: [],
			selectOptions: this.getMultiSelectOptions(),
			categoryNames: [], 
		});
	}

	// Called when create button is clicked
	createClick(ingridientsData) {

		let ingridientCategories = [];

		for (let category of this.state.itemCategories) {
			ingridientCategories.push({IngridientId: this.state.itemID, CategoryId: category.value});
		}

		// Sending HTTP POST request to the server
		// with data from state
		fetch(constants.API_URL + "IngridientData", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			// Content of the request, will be converted to
			// the IngridientData instance on API side
			// and passed to HTTP Post method
			body: JSON.stringify({
				Name: this.state.itemName,
				Calories: this.state.itemCalories,
				Proteins: this.state.itemProteins,
				Carbohydrates: this.state.itemCarbohydrates,
				Fats: this.state.itemFats,
				ingridientCategory: ingridientCategories,
		})})
			.then(res => res.json())
			.then(
				result => {
					// Refreshing data
					this.refreshList();
				},
				error => {
					alert("Failed");
				}
			);
	}

	editClick(row) {
		let rowCategories = this.state.ingridientsData.find(i => i.Id === row.Id)
			.IngridientCategory.map(i => i.Category);
		let temp = []

		for(let category of rowCategories) {
			temp.push({ label: category.Name, value: category.Id });
		}

		this.setState({
			modalTitle: "Editing Ingridient",
			itemID: row.Id,
			itemName: row.Name,
			itemCalories: row.Calories,
			itemProteins: row.Proteins,
			itemCarbohydrates: row.Carbohydrates,
			itemFats: row.Fats,
			itemCategories: temp,
			selectOptions: this.getMultiSelectOptions(),
			categoryNames: rowCategories.map(i => i.Name),
		});
	}

	// Called when update button is clicked
	updateClick() {
		let ingridientCategories = [];

		for (let category of this.state.itemCategories) {
			ingridientCategories.push({IngridientId: this.state.itemID, CategoryId: category.value});
		}

		console.log(ingridientCategories);

		// Sending HTTP PUT request to the server
		// with data from state
		fetch(constants.API_URL + "IngridientData", {
			method: "PUT",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				Id: this.state.itemID,
				Name: this.state.itemName,
				Calories: this.state.itemCalories,
				Proteins: this.state.itemProteins,
				Carbohydrates: this.state.itemCarbohydrates,
				Fats: this.state.itemFats,
				ingridientCategory: ingridientCategories,
			})
		})
			.then(res => res.json())
			.then(
				result => {
					this.refreshList();
				},
				error => {
					alert("Failed");
				}
			);
	}

	// Called when delete button is clicked
	deleteClick(id) {
		// Confirmation pop-up
		if (window.confirm("Are you sure?")) {
			// Sending request to delete item from DB with current id
			fetch(constants.API_URL + "IngridientData/" + id, {
				method: "DELETE",
				headers: {
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						this.refreshList();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// HTTP GET request, retrieves data from server
	// and saves it to the component state
	refreshList() {
		fetch(constants.API_URL + "IngridientData")
			.then(response => response.json())
			.then(data => {
				this.setState({ ingridientsData: data });
			});

		fetch(constants.API_URL + "Category")
			.then(response => response.json())
			.then(data => {
				this.setState({ categories: data });
			});
	}

	// Don't exactly know what is it :)
	// Maybe it is called when component is rendered ¯\_(ツ)_/¯
	componentDidMount() {
		this.refreshList();
	}

	// Next 5 functions called when user types
	// something in input fields of modal window.
	// They are saving this data to specified state variables
	changeName = e => {
		this.setState({ itemName: e.target.value });
	};

	changeCalories = e => {
		this.setState({ itemCalories: e.target.value });
	};

	changeFats = e => {
		this.setState({ itemFats: e.target.value });
	};

	changeCarbohydrates = e => {
		this.setState({ itemCarbohydrates: e.target.value });
	};

	changeProteins = e => {
		this.setState({ itemProteins: e.target.value });
	};

	changeCategory = e => {
		this.setState({ 
			itemCategories: e,
			categoryNames: e.map(e => e.label),
		});

	};

	getMultiSelectOptions() {
		let res = [];

		for (let category of this.state.categories) {
			res.push({label: category.Name, value: category.Id });
		}

		return res;
	}

	// Possibly main function of the component
	// Return statement of this function describes what
	// will be displayed in place where this component is used
	render() {
		// This part describes what will be displayed
		return (
			<div className="container">
				<div style={{ width: 80 + "vw" }}>
					<h3 className="m-5">This is Ingridients page</h3>

					{/* Main table */}
					<div className="container mt-5">
						<Table 
							className="table table-striped auto__table text-center" 
							striped bordered hover size="lg">
							<thead>
								<tr>
									<th>
										ID
										<Button className="btn-light">
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>
										Name
										<Button className="btn-light">
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>
										Calories
										<Button className="btn-light">
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>
										Proteins
										<Button className="btn-light">
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>
										Fats
										<Button className="btn-light">
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>
										Carbohydrates
										<Button className="btn-light">
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>Options</th>
								</tr>
							</thead>
							<tbody>
								{this.state.ingridientsData.map(e => (
									<tr key={e.Id}>
										<td>{e.Id}</td>
										<td>{e.Name}</td>
										<td>{e.Calories}</td>
										<td>{e.Proteins}</td>
										<td>{e.Fats}</td>
										<td>{e.Carbohydrates}</td>
										<td>
											<Button 
												className="m-2" 
												variant="outline-dark"
												onClick={() => this.editClick(e)}
												data-bs-toggle="modal"
												data-bs-target="#exampleModal">
												Edit
											</Button>
											<Button className="m-2" onClick={() => this.deleteClick(e.Id)} variant="outline-danger">
												Delete
											</Button>
										</td>
									</tr>
								))}
							</tbody>
						</Table>
					</div>

					{/* Three buttons to perform basic operations */}
					<button
						type="button"
						className="btn btn-dark m-2 float-end"
						// Click will trigger modal
						data-bs-toggle="modal"
						// Id of modal to be triggered
						data-bs-target="#exampleModal"
						onClick={() => this.addClick()}>
						Add ingridient data
					</button>

					{/* Modal window component */}
					<div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
						<div className="modal-dialog modal-lg modal-dialog-centered">
							<div className="modal-content">
								<div className="modal-header">
									<h5 className="modal-title">{this.state.modalTitle}</h5>
									<button
										type="button"
										className="btn-close"
										data-bs-dismiss="modal"
										aria-label="Close"></button>
								</div>
								<div className="modal-body">
									{/* Inputs for item properties
                                    value: assigns data from component state
                                    onChange: calls specified function to update state */}
									<div className="input-group mb-3">
										<span className="input-group-text">Ingridient Name</span>
										<input
											type="text"
											className="form-control"
											value={this.state.itemName}
											onChange={this.changeName}
										/>
									</div>
									<div className="input-group mb-3">
										<span className="input-group-text">Ingridient Calories</span>
										<input
											type="text"
											className="form-control"
											value={this.state.itemCalories}
											onChange={this.changeCalories}
										/>
									</div>
									<div className="input-group mb-3">
										<span className="input-group-text">Ingridient Carbohydrates</span>
										<input
											type="text"
											className="form-control"
											value={this.state.itemCarbohydrates}
											onChange={this.changeCarbohydrates}
										/>
									</div>
									<div className="input-group mb-3">
										<span className="input-group-text">Ingridient Fats</span>
										<input
											type="text"
											className="form-control"
											value={this.state.itemFats}
											onChange={this.changeFats}
										/>
									</div>
									<div className="input-group mb-3">
										<span className="input-group-text">Ingridient Proteins</span>
										<input
											type="text"
											className="form-control"
											value={this.state.itemProteins}
											onChange={this.changeProteins}
										/>
									</div>
									<div className="input-group mb-3">
										{/* <span className="input-group-text">Ingridient Category</span> */}
										
										<MultiSelect
											className="input-group-text"
											options={this.state.selectOptions}
											value={this.state.itemCategories}
											onChange={this.changeCategory}
											labelledBy="Ingridient Category"
										/>
										<input
											type="text"
											className="form-control"
											value={JSON.stringify(this.state.categoryNames)}
											onChange={this.changeProteins}
											disabled={true}
										/>
									</div>

									{/* If selected item id == 0 Than we need to add new item */}
									{this.state.itemID === 0 ? (
										<button
											type="button"
											className="btn btn-primary float-start"
											onClick={() => this.createClick()}>
											Create
										</button>
									) : null}

									{/* If selected item id !== 0 Than we need to updating existing item */}
									{this.state.itemID !== 0 ? (
										<button
											type="button"
											className="btn btn-primary float-start"
											onClick={() => this.updateClick()}>
											Update
										</button>
									) : null}
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		);
	}
}
