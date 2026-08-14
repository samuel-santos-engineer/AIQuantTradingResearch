namespace AIQuantTradingResearch.Application.Research;

public interface IObservationSource
{
    ObservationSourceResult GetObservations(ResearchRequest request);
}
