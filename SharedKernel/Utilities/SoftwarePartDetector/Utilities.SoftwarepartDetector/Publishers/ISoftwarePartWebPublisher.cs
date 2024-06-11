using Utilities.SoftwarepartDetector.DataModel;

namespace Utilities.SoftwarepartDetector.Publishers;

public interface ISoftwarePartPublisher
{
    Task PublishAsync(SoftwarePart softwarePart);
}